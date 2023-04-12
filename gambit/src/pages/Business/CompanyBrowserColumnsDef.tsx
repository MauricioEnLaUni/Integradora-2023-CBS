import CompanyBrowserDto from "./CompanyBrowserType";

const linkToDetails = (params: { row: CompanyBrowserDto }) =>
  <a href={`localhost:5173/clients/${params.row.id}`}>
    {params.row.name}
  </a>

const CompanyBrowserColumnsDef = (canEdit: boolean) => {
  return [
    { field: 'id', headerName: 'ID', width: 90 },
    {
      field: 'name',
      headerName: 'Companía',
      width: 200,
      editable: canEdit,
      renderCell: linkToDetails
    },
    {
      field: 'activity',
      headerName: 'Giro',
      width: 200,
      editable: canEdit,
    },
    {
      field: 'relation',
      headerName: 'Relación',
      width: 300,
      editable: canEdit,
    }
  ];
}

export default CompanyBrowserColumnsDef;