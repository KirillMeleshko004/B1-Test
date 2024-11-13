import { turnoverDocumentDetails } from "../../utils/interfaces/excelAPIInterfaces";
import { getDetailsMarkDown } from "../../utils/tableMapper/turnoverDocumentMapper";
import DetailsHeader from "../DetailsHeader";
import Table from "../Table";
import styles from "./styles.module.css";

type tableProps = {
  data: turnoverDocumentDetails | undefined;
};

function DetailsTable({ data }: tableProps) {
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
    data && (
      <>
        <div className={styles.title}>
          <div>{data?.bankName}</div>
          <div>{data?.title}</div>
          <div>
            {new Date(data.date).toLocaleDateString("en-Us", dateFormatOptions)}
          </div>
          <div>{data?.currency}</div>
        </div>
        <>
          <Table>
            <DetailsHeader></DetailsHeader>
            {getDetailsMarkDown(data)}
          </Table>
        </>
      </>
    )
  );
}

export default DetailsTable;
