import { editor, Uri } from 'monaco-editor';
import { setDiagnosticsOptions } from 'monaco-yaml';

import defaultSchemaUri from './schema-inventory.json';

const defaultSchema = {
  uri: defaultSchemaUri,
  fileMatch: ['my-inventory.yaml'],
};

setDiagnosticsOptions({
  enableSchemaRequest: true,
  hover: true,
  completion: true,
  validate: true,
  format: true,
  schemas: [defaultSchema],
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
`;

editor.create(document.getElementById('editor'), {
  automaticLayout: true,
  model: editor.createModel(value, 'yaml', Uri.parse('my-inventory.yaml')),
  theme: window.matchMedia('(prefers-color-scheme: dark)').matches ? 'vs-dark' : 'vs-light',
});
