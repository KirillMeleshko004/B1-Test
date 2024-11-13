import { useNavigate } from "react-router-dom";
import TableData from "../TableData";
import styles from "./styles.module.css";

type RowProps<T> = {
  data: T;
  hasNavigation: boolean;
};

function TableRow<T>({ data, hasNavigation }: RowProps<T>) {
  const dateFormatOptions = {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "numeric",
    minute: "2-digit",
    second: "2-digit",
    hour12: false,
  } as const;

  const navigate = useNavigate();

  return (
    data && (
      <tr
        className={styles.tr}
        onClick={() => {
          if (!hasNavigation) return;
          navigate(`/documents/${data["id" as keyof T]}`);
        }}>
        {Object.keys(data).map((key, index) => {
          const value = data[key as keyof T];
          let stringValue = "";
          if (key.toLowerCase().includes("date")) {
            stringValue = new Date(value as string).toLocaleDateString(
              "En-us",
              dateFormatOptions
            );
          } else stringValue = value as string;

          return <TableData key={index}>{stringValue}</TableData>;
        })}
      </tr>
    )
  );
}

export default TableRow;
