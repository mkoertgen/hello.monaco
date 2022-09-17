import { editor, Uri } from "monaco-editor";
import { setDiagnosticsOptions } from "monaco-yaml";

import schemaUrl from "./schema-inventory.json";

const defaultSchema = {
  uri: schemaUrl,
  fileMatch: ["my-inventory.yaml"],
};

setDiagnosticsOptions({
  enableSchemaRequest: true,
  hover: true,
  completion: true,
  validate: true,
  format: true,
  schemas: [defaultSchema],
});

// Access module export via webpack library output, cf.: https://stackoverflow.com/a/52923799/2592915
export default class Editor {
  constructor(elementId, value) {
    const element = document.getElementById(elementId);
    this.editor = editor.create(element, {
      automaticLayout: true,
      model: editor.createModel(value, "yaml", Uri.parse("my-inventory.yaml")),
      theme: window.matchMedia("(prefers-color-scheme: dark)").matches
        ? "vs-dark"
        : "vs-light",
    });
  }

  getValue() {
    return this.editor.getModel().getValue();
  }

  isValid() {
    return editor.getModelMarkers({}).length === 0;
  }

  getValidationErrors() {
    return editor
      .getModelMarkers({})
      .map((m) => `${m.message} (${m.startLineNumber}:${m.startColumn})`)
      .join("\n");
  }

  onChangedValid(callback) {
    editor.onDidChangeMarkers((e) => {
      callback();
    });
  }

  getEditor() {
    return this.editor;
  }

  static getSchemaUrl() {
    return schemaUrl;
  }

  static getApi() {
    return editor;
  }
}
