import TableHead from "../TableHead";

function DetailsHeader() {
  return (
    <thead>
      <tr>
        <TableHead
          rowSpan={2}
          colSpan={1}>
          Б/сч
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={2}>
          ВХОДЯЩЕЕ САЛЬДО
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={2}>
          ОБОРОТЫ
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={2}>
          ИСХОДЯЩЕЕ САЛЬДО
        </TableHead>
      </tr>
      <tr>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Актив
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Пассив
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Дебет
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Кредит
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Актив
        </TableHead>
        <TableHead
          rowSpan={1}
          colSpan={1}>
          Пассив
        </TableHead>
      </tr>
    </thead>
  );
}

export default DetailsHeader;
