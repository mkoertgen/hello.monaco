import HtmlWebPackPlugin from 'html-webpack-plugin';
import MonacoWebpackPlugin from 'monaco-editor-webpack-plugin';

export default {
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, `wwwroot/dist` )
  },
  module: {
    rules: [
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.ttf$/,
        type: 'asset',
      },
      {
        test: /schema.*\.json$/,
        type: 'asset/resource',
      },
    ],
  },
  plugins: [
    new HtmlWebPackPlugin(),
    new MonacoWebpackPlugin({
      languages: ['yaml'],
      customLanguages: [
        {
          label: 'yaml',
          entry: 'monaco-yaml',
          worker: {
            id: 'monaco-yaml/yamlWorker',
            entry: 'monaco-yaml/yaml.worker',
          },
        },
      ],
    }),
  ],
};
