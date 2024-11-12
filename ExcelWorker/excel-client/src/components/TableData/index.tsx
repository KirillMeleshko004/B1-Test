import { ReactNode } from "react";
import styles from "./styles.module.css";

function TableData({ children }: { children: ReactNode }) {
  return <td className={styles.td}>{children}</td>;
}

export default TableData;
