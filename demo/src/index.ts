import './index.css';

import { ILanguageFeaturesService } from 'monaco-editor/esm/vs/editor/common/services/languageFeatures.js';
import { OutlineModel } from 'monaco-editor/esm/vs/editor/contrib/documentSymbols/browser/outlineModel.js';
import {
  editor,
  Environment,
  languages,
  MarkerSeverity,
  Position,
  Range,
  Uri,
} from 'monaco-editor/esm/vs/editor/editor.api.js';
import { StandaloneServices } from 'monaco-editor/esm/vs/editor/standalone/browser/standaloneServices.js';
import { SchemasSettings, setDiagnosticsOptions } from 'monaco-yaml';

// NOTE: This will give you all editor featues. If you would prefer to limit to only the editor
// features you want to use, import them each individually. See this example: (https://github.com/microsoft/monaco-editor-samples/blob/main/browser-esm-webpack-small/index.js#L1-L91)
import 'monaco-editor';

import defaultSchemaUri from './schema-inventory.json';

declare global {
  interface Window {
    MonacoEnvironment: Environment;
  }
}

window.MonacoEnvironment = {
  getWorker(moduleId, label) {
    switch (label) {
      case 'editorWorkerService':
        return new Worker(new URL('monaco-editor/esm/vs/editor/editor.worker', import.meta.url));
      case 'yaml':
        return new Worker(new URL('monaco-yaml/yaml.worker', import.meta.url));
      default:
        throw new Error(`Unknown label ${label}`);
    }
  },
};

const defaultSchema: SchemasSettings = {
  uri: defaultSchemaUri,
  fileMatch: ['my-inventory.yaml'],
};

setDiagnosticsOptions({
  schemas: [defaultSchema],
  validate: true,
  enableSchemaRequest: true,
  format: true,
  hover: true,
  completion: true,
});

const value = `all:
  vars:
    stage: dev
  children:
    gateway:
      hosts:
        my-gateway:
          ansible_host: 192.168.1.101
      vars:
        iotHub: 
          enabled: true
          name: my-iot-hub
          device:
            id: MyDevice01
            key: MyDevice01Key
        elastic:
          enabled: true
          hosts: https://elastic.mycloud.com:443
          username: my-gateway
          password: SuperSecret
`.replace(/:$/m, ': ');

const ed = editor.create(document.getElementById('editor'), {
  automaticLayout: true,
  model: editor.createModel(value, 'yaml', Uri.parse('my-inventory.yaml')),
  theme: window.matchMedia('(prefers-color-scheme: dark)').matches ? 'vs-dark' : 'vs-light',
});

function* iterateSymbols(
  symbols: languages.DocumentSymbol[],
  position: Position,
): Iterable<languages.DocumentSymbol> {
  for (const symbol of symbols) {
    if (Range.containsPosition(symbol.range, position)) {
      yield symbol;
      if (symbol.children) {
        yield* iterateSymbols(symbol.children, position);
      }
    }
  }
}

ed.onDidChangeCursorPosition(async (event) => {
  const breadcrumbs = document.getElementById('breadcrumbs');
  const { documentSymbolProvider } = StandaloneServices.get(ILanguageFeaturesService);
  const outline = await OutlineModel.create(documentSymbolProvider, ed.getModel());
  const symbols = outline.asListOfDocumentSymbols();
  while (breadcrumbs.lastChild) {
    breadcrumbs.lastChild.remove();
  }
  for (const symbol of iterateSymbols(symbols, event.position)) {
    const breadcrumb = document.createElement('span');
    breadcrumb.setAttribute('role', 'button');
    breadcrumb.classList.add('breadcrumb');
    breadcrumb.textContent = symbol.name;
    breadcrumb.title = symbol.detail;
    if (symbol.kind === languages.SymbolKind.Array) {
      breadcrumb.classList.add('array');
    } else if (symbol.kind === languages.SymbolKind.Module) {
      breadcrumb.classList.add('object');
    }
    breadcrumb.addEventListener('click', () => {
      ed.setPosition({
        lineNumber: symbol.range.startLineNumber,
        column: symbol.range.startColumn,
      });
      ed.focus();
    });
    breadcrumbs.append(breadcrumb);
  }
});

editor.onDidChangeMarkers(([resource]) => {
  const problems = document.getElementById('problems');
  const markers = editor.getModelMarkers({ resource });
  while (problems.lastChild) {
    problems.lastChild.remove();
  }
  for (const marker of markers) {
    if (marker.severity === MarkerSeverity.Hint) {
      continue;
    }
    const wrapper = document.createElement('div');
    wrapper.setAttribute('role', 'button');
    const codicon = document.createElement('div');
    const text = document.createElement('div');
    wrapper.classList.add('problem');
    codicon.classList.add(
      'codicon',
      marker.severity === MarkerSeverity.Warning ? 'codicon-warning' : 'codicon-error',
    );
    text.classList.add('problem-text');
    text.textContent = marker.message;
    wrapper.append(codicon, text);
    wrapper.addEventListener('click', () => {
      ed.setPosition({ lineNumber: marker.startLineNumber, column: marker.startColumn });
      ed.focus();
    });
    problems.append(wrapper);
  }
});
