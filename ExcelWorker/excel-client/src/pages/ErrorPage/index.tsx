import { useRouteError } from "react-router-dom";
import styles from "./styles.module.css";

export default function ErrorPage() {
  const error: any = useRouteError();
  console.error(error);

  return (
    <main className={styles.main}>
      <div>ErrorPage</div>
      <div>{error.statusText || error.message}</div>
    </main>
  );
}
