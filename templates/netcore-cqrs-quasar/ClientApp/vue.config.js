const path = require("path");

module.exports = {
  outputDir: path.resolve(__dirname, "../API/ClientApp/dist"),
  pluginOptions: {
    quasar: {
      importStrategy: "kebab",
      rtlSupport: false,
    },
  },
  transpileDependencies: ["quasar"],
};
