import path from "path";
import MonacoWebpackPlugin from "monaco-editor-webpack-plugin";

export default {
  entry: {
    editor: "./src/editor.js",
  },
  performance: {
    //hints: true,
    assetFilter: function(asset) {
      // monaco-editor is big! Skip warnings for it.
      const toSkip = ['editor.entry.js', 'yaml.worker.js'];
      return !toSkip.some((s) => asset.includes(s));
    }
  },
  output: {
    path: path.resolve("..", "wwwroot", "dist"),
    filename: "[name].entry.js",
    library: "[name]Library",
  },
  mode: "development",
  module: {
    rules: [
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
      {
        test: /\.ttf$/,
        type: "asset",
      },
      {
        test: /schema.*\.json$/,
        type: "asset/resource",
      },
    ],
  },
  plugins: [
    new MonacoWebpackPlugin({
      languages: ["yaml"],
      customLanguages: [
        {
          label: "yaml",
          entry: "monaco-yaml",
          worker: {
            id: "monaco-yaml/yamlWorker",
            entry: "monaco-yaml/yaml.worker",
          },
        },
      ],
    }),
  ],
};
