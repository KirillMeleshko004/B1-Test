import { useParams } from "react-router";
import styles from "./styles.module.css";
import DetailsTable from "../../components/DetailsTable";
import { useEffect, useRef, useState } from "react";
import { turnoverDocumentDetails } from "../../utils/interfaces/excelAPIInterfaces";
import { documentsEndpoint } from "../../constants/apiConstatns";
import { getSingleData } from "../../api/apiHelpers";

function TablePage() {
  const { id } = useParams();

  const [data, setData] = useState<turnoverDocumentDetails>();

  const shouldRequest = useRef(true);

  useEffect(() => {
    if (!shouldRequest.current) return;
    shouldRequest.current = false;

    fetchData();

    return (): void => {};
  }, []);

  async function fetchData() {
    let result = await getSingleData<turnoverDocumentDetails>(
      `${documentsEndpoint}/${id}/details`
    );

    if (result.isSuccessful == true) {
      setData(result.data);
    } else {
      alert(result.errorMessage);
    }
  }

  return (
    <main className={styles.main}>
      <DetailsTable data={data}></DetailsTable>
    </main>
  );
}

export default TablePage;
