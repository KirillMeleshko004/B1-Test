export type turnoverDocument = {
  id: string;
  bankName: string;
  title: string;
  date: Date;
  currency: string;
  creationDate: Date;
};

export type turnoverDocumentDetails = {
  id: string;
  bankName: string;
  title: string;
  date: Date;
  currency: string;
  creationDate: Date;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;

  summaryClasses: summaryClass[];
};

export type summaryClass = {
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
  number: number;

  openingBalanceAsset: number;
  openingBalanceLiability: number;

  turnoverDebit: number;
  turnoverCredit: number;

  closingBalanceAsset: number;
  closingBalanceLiability: number;

  account: account[];
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
