import { turnoverDocument } from "../../utils/interfaces/ExcelAPIInterfaces";
import TableData from "../TableData";
import styles from "./styles.module.css";

function TableRow({ document }: { document: turnoverDocument }) {
  const dateFormatOptions = {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "numeric",
    minute: "2-digit",
    second: "2-digit",
    hour12: false,
  } as const;

  return (
    <tr className={styles.tr}>
      <TableData>{document.id}</TableData>
      <TableData>{document.bankName}</TableData>
      <TableData>
        {new Date(document.date).toLocaleDateString("En-us", dateFormatOptions)}
      </TableData>
      <TableData>{document.currency}</TableData>
      <TableData>
        {new Date(document.creationDate).toLocaleDateString(
          "En-us",
          dateFormatOptions
        )}
      </TableData>
    </tr>
  );
}

export default TableRow;
