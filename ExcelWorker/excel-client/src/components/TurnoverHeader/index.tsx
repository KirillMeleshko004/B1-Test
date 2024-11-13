import TableHead from "../TableHead";

function TurnoverHeader() {
  const heading = [
    "ID",
    "Название банка",
    "Заголовок",
    "Дата отчета",
    "Валюта",
    "Дата создания",
  ];

  return (
    <thead>
      <tr>
        {heading.map((h, ind) => {
          return (
            <TableHead
              key={ind}
              rowSpan={1}
              colSpan={1}>
              {h}
            </TableHead>
          );
        })}
      </tr>
    </thead>
  );
}

export default TurnoverHeader;
