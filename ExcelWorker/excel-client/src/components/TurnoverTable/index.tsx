import { turnoverDocument } from "../../utils/interfaces/excelAPIInterfaces";
import Table from "../Table";
import TableRow from "../TableRow";
import TurnoverHeader from "../TurnoverHeader";
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
    data && (
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
        <Table>
          <TurnoverHeader></TurnoverHeader>
          <tbody>
            {data.map((el, ind) => {
              return (
                <TableRow
                  hasNavigation={true}
                  data={el}
                  key={ind}></TableRow>
              );
            })}
          </tbody>
        </Table>
      </>
    )
  );
}

export default TurnoverTable;
