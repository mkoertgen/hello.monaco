import { Counter } from "./components/Counter";
import { FetchWeather } from "./components/FetchWeather";
import { FetchPosts } from "./components/FetchPosts";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-weather',
    element: <FetchWeather />
  },
  {
    path: '/fetch-posts',
    element: <FetchPosts />
  }
];

export default AppRoutes;
