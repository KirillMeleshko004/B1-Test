import { turnoverDocument } from "../../utils/interfaces/ExcelAPIInterfaces";
import TableHeader from "../TableHeader";
import TableRow from "../TableRow";
import styles from "./styles.module.css";

type tableProps = {
  data: turnoverDocument[] | undefined;
  page: number;
  totalPages: number | undefined;
  nextPage: () => void;
  prevPage: () => void;
  hasPrev: boolean;
  hasNext: boolean;
};

function TurnoverTable({
  data,
  page,
  totalPages,
  nextPage,
  prevPage,
  hasPrev,
  hasNext,
}: tableProps) {
  return (
    <>
      <div className={styles.paging}>
        <div>
          Page {page} of {totalPages}
        </div>
        <button
          onClick={prevPage}
          disabled={!hasPrev}>
          Prev
        </button>
        <button
          onClick={nextPage}
          disabled={!hasNext}>
          Next
        </button>
      </div>
      <table className={styles.table}>
        <TableHeader
          columns={[
            "Id",
            "Bank name",
            "Date",
            "Currency",
            "Creation date",
          ]}></TableHeader>

        <tbody>
          {data?.map((d) => {
            return (
              <TableRow
                key={d.id}
                document={d}></TableRow>
            );
          })}
        </tbody>
      </table>
    </>
  );
}

export default TurnoverTable;
