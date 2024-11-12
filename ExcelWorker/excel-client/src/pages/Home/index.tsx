import { useEffect, useState } from "react";
import FileImport from "../../components/FileImport";
import TurnoverTable from "../../components/TurnoverTable";
import styles from "./styles.module.css";
import { turnoverDocument } from "../../utils/interfaces/ExcelAPIInterfaces";
import { getData, PageMetadata } from "../../api/apiHelpers";
import { documentsEndpoint } from "../../constants/apiConstatns";

function HomePage() {
  const [data, setData] = useState<turnoverDocument[]>();

  const [paging, setPaging] = useState<PageMetadata>({
    CurrentPage: 1,
    TotalPages: 1,
    PageSize: 10,
    TotalCount: 1,
    HasNext: false,
    HasPrevious: false,
  });
  const [totalPages, setTotalPages] = useState<number>();

  const [shouldRequest, setShouldRequest] = useState(true);

  useEffect(() => {
    if (!shouldRequest) return;
    setShouldRequest(false);

    async function fetchData() {
      let result = await getData<turnoverDocument[]>(
        documentsEndpoint,
        paging.CurrentPage,
        10
      );

      if (result.isSuccessful == true) {
        setData(result.data);
        setPaging(result.pageData);
        setTotalPages(result.pageData.TotalPages);
        console.log(result.pageData);
      } else {
        alert(result.errorMessage);
      }
    }

    fetchData();
  }, [shouldRequest, paging]);

  function dataUploaded(td: turnoverDocument) {
    if (!data) {
      setData([td]);
      return;
    }

    setShouldRequest(true);
  }

  function nextPage() {
    if (!paging.TotalPages || paging.CurrentPage >= paging.TotalPages) {
      return;
    }

    setShouldRequest(true);
    setPaging({ ...paging, CurrentPage: paging.CurrentPage + 1 });
  }

  function prevPage() {
    if (paging.CurrentPage <= 1) {
      return;
    }

    setShouldRequest(true);
    setPaging({ ...paging, CurrentPage: paging.CurrentPage - 1 });
  }

  return (
    <main className={styles.main}>
      <FileImport uploaded={dataUploaded}></FileImport>
      <TurnoverTable
        data={data}
        page={paging.CurrentPage}
        totalPages={totalPages}
        nextPage={nextPage}
        prevPage={prevPage}
        hasNext={paging.HasNext}
        hasPrev={paging.HasPrevious}></TurnoverTable>
    </main>
  );
}

export default HomePage;
