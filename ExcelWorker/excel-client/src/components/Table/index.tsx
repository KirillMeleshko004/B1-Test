import styles from "./styles.module.css";
import { ReactNode } from "react";

type TableProps = {
  children: ReactNode | undefined;
};

function Table({ children }: TableProps) {
  return <table className={styles.table}>{children}</table>;
}

export default Table;
