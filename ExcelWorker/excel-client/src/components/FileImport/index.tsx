import { ChangeEvent, FormEvent, useRef } from "react";
import styles from "./styles.module.css";
import { postFormData } from "../../api/apiHelpers";
import { uploadEndpoint } from "../../constants/apiConstatns";
import { turnoverDocument } from "../../utils/interfaces/excelAPIInterfaces";

type fileImportProps = {
  uploaded: () => void;
};

function FileImport({ uploaded }: fileImportProps) {
  var file = useRef<string>();

  async function submit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData();

    const form = e.target! as HTMLFormElement;

    const excelFile = form["excelFile"].files[0];

    formData.append("ExcelDocument", excelFile, excelFile.name);

    let result = await postFormData<turnoverDocument>(uploadEndpoint, formData);

    if (result.isSuccessful) {
      uploaded();
      form["excelFile"].value = null;
      alert("Uploaded");
    } else {
      alert(result.errorMessage);
    }
  }

  function fileChanged(e: ChangeEvent<HTMLInputElement>) {
    if (
      (e.target as HTMLInputElement).files &&
      (e.target as HTMLInputElement).files!.length
    ) {
      const target = e.target as HTMLInputElement;
      let url = target.files![0] && URL.createObjectURL(target.files![0]);

      file.current = url;
    }
  }

  return (
    <form
      className={styles.importContainer}
      onSubmit={submit}>
      Import new excel file
      <input
        type="file"
        accept=".xls,.xlsx"
        name="excelFile"
        onChange={fileChanged}></input>
      <input
        className={styles.uploadBtn}
        type="submit"
        value="Upload"></input>
    </form>
  );
}

export default FileImport;
