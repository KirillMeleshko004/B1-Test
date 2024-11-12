import { ReactNode } from "react";
import styles from "./styles.module.css";

function TableHead({ children }: { children: ReactNode }) {
  return <th className={styles.th}>{children}</th>;
}

export default TableHead;
