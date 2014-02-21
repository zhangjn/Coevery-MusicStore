using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Coevery.Mvc.Routes;

namespace MusicStore {
    public class Routes : IRouteProvider {
        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Priority = 100,
                    Route = new Route("",
                        new RouteValueDictionary {
                            {"area", "MusicStore"},
                            {"controller", "Home"},
                            {"action", "Index"}
                        }, new RouteValueDictionary(),
                        new RouteValueDictionary {{"area", "MusicStore"}},
                        new MvcRouteHandler())
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach(RouteDescriptor route in GetRoutes()) {
                routes.Add(route);
            }
        }
    }
}