import { ReactNode } from "react";
import styles from "./styles.module.css";

type HeadProps = {
  children: ReactNode;
  rowSpan: number;
  colSpan: number;
};

function TableHead({ children, rowSpan = 1, colSpan = 1 }: HeadProps) {
  return (
    <th
      className={styles.th}
      rowSpan={rowSpan}
      colSpan={colSpan}>
      {children}
    </th>
  );
}

export default TableHead;
