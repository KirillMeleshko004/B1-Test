import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { HOME, TABLE } from "./constants/routes.ts";
import HomePage from "./pages/Home/index.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./pages/ErrorPage/index.tsx";
import TablePage from "./pages/TablePage/index.tsx";

const router = createBrowserRouter([
  {
    path: HOME,
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: HOME,
        element: <HomePage />,
      },
      {
        path: TABLE,
        element: <TablePage />,
      },
    ],
  },
]);

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router}></RouterProvider>
  </StrictMode>
);
