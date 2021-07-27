import * as _ from "lodash";
import { RouteConfig } from "vue-router";

interface MetaRoute {
  name: string;
  meta: any;
}
interface RequiredMeta {
  meta: any;
  exclusionRoutes: string[];
}

function getMeta(
  routeName: string,
  requiredMeta: RequiredMeta,
  optionalMeta: MetaRoute[]
): any {
  const index = optionalMeta.findIndex(
    (i) => i.name.toLocaleLowerCase() === routeName.toLocaleLowerCase()
  );
  let meta: any = {};
  if (index !== -1) {
    meta = { ...meta, ...optionalMeta[index].meta };
  }
  if (
    !requiredMeta.exclusionRoutes
      .map((i) => i.toLowerCase())
      .includes(routeName.toLowerCase())
  ) {
    meta = { ...meta, ...requiredMeta.meta };
  }
  return meta;
}
// add default route.
function computeRoutes(
  home: string,
  requiredMeta: RequiredMeta,
  optionalMeta: MetaRoute[]
): RouteConfig[] {
  const routes: RouteConfig[] = [];
  // compute routes dynamically.
  const req = require.context("@/views", true, /\.(vue)$/i, "lazy");
  req.keys().map((key) => {
    const path = `/${key
      .slice(2)
      .replace(".vue", "")
      .split("/")
      .reverse()
      .join("-")
      .toLocaleLowerCase()}`;
    const name = _.upperFirst(_.camelCase(path));

    routes.push({
      path: name === home ? "/" : path,
      name,
      component: () => import(`@/views/${key.slice(2)}`),
      meta: getMeta(name, requiredMeta, optionalMeta),
    });
  });
  return routes;
}

const routes = computeRoutes(
  "Home",
  { meta: { requiresAuth: true }, exclusionRoutes: ["Login", "Register"] },
  [
    { name: "Login", meta: { layout: "fullscreen" } },
    { name: "Register", meta: { layout: "fullscreen" } },
  ]
);
console.log(routes);
export default routes;
