export type turnoverDocument = {
  id: string;
  bankName: string;
  title: string;
  date: string;
  currency: string;
  creationDate: string;
};

export type turnoverDocumentDetails = {
  id: string;
  bankName: string;
  title: string;
  date: string;
  currency: string;
  creationDate: string;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;

  summaryClasses: summaryClass[];
};

export type summaryClass = {
  id: string;
  number: number;
  title: string;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;

  accountSummaries: accountsSummary[];
};

export type accountsSummary = {
  id: string;
  number: number;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;

  accounts: account[];
};

export type account = {
  number: number;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;
};
