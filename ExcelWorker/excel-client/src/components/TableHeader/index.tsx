import TableHead from "../TableHead";

function TableHeader({ columns }: { columns: string[] }) {
  const listElements = columns.map((c, i) => {
    return <TableHead key={i}>{c}</TableHead>;
  });

  return (
    <thead>
      <tr>{listElements}</tr>
    </thead>
  );
}

export default TableHeader;
