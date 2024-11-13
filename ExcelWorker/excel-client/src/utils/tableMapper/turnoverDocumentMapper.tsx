import TableHead from "../../components/TableHead";
import TableRow from "../../components/TableRow";
import {
  account,
  accountsSummary,
  summaryClass,
  turnoverDocumentDetails,
} from "../interfaces/excelAPIInterfaces";

export function getDetailsMarkDown(td: turnoverDocumentDetails) {
  var summaryClasses = td.summaryClasses.map(getSummaryClassMarkDown);

  return (
    <>
      {summaryClasses}
      <tbody>
        <TableRow
          hasNavigation={false}
          key={td.id}
          data={[
            "БАЛАНС",
            td.openingBalanceAsset,
            td.openingBalanceLiability,
            td.turnoverDebit,
            td.turnoverCredit,
            td.closingBalanceAsset,
            td.closingBalanceLiability,
          ]}></TableRow>
      </tbody>
    </>
  );
}

function getSummaryClassMarkDown(sc: summaryClass) {
  const header = (
    <thead>
      <tr>
        <TableHead
          rowSpan={1}
          colSpan={7}>
          {sc.title}
        </TableHead>
      </tr>
    </thead>
  );

  const accountsSummaries = sc.accountSummaries.map(
    getAccountSummariesMarkdown
  );

  return (
    <>
      {header}
      <tbody>
        {accountsSummaries}
        <TableRow
          hasNavigation={false}
          key={sc.id}
          data={[
            "ПО КЛАССУ",
            sc.openingBalanceAsset,
            sc.openingBalanceLiability,
            sc.turnoverDebit,
            sc.turnoverCredit,
            sc.closingBalanceAsset,
            sc.closingBalanceLiability,
          ]}></TableRow>
      </tbody>
    </>
  );
}

function getAccountSummariesMarkdown(as: accountsSummary) {
  const accounts = as.accounts.map(getAccountMarkdown);
  return (
    <>
      {accounts}
      <TableRow
        hasNavigation={false}
        key={as.id}
        data={[
          as.number,
          as.openingBalanceAsset,
          as.openingBalanceLiability,
          as.turnoverDebit,
          as.turnoverCredit,
          as.closingBalanceAsset,
          as.closingBalanceLiability,
        ]}></TableRow>
    </>
  );
}

function getAccountMarkdown(a: account) {
  return (
    <TableRow
      data={a}
      hasNavigation={false}></TableRow>
  );
}
