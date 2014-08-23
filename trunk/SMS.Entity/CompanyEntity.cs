using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;

namespace VPR.Entity
{
    public class CompanyEntity : ICompany
    {
        #region ICompany Members

        public IAddress CompAddress
        {
            get;
            set;
        }

        public string CompName
        {
            get;
            set;
        }

        public string CompPhone
        {
            get;
            set;
        }

        public int? fk_StateID
        {
            get;
            set;
        }

        public int fk_CountryID
        {
            get;
            set;
        }

        public string ContactPerson
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string StateName
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string PIN
        {
            get;
            set;
        }
        #endregion

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CompanyEntity()
        {
            this.CompAddress = new AddressEntity();
        }

        public CompanyEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.CompName = Convert.ToString(reader["CompName"]);
            this.CompAddress = new AddressEntity(reader);
            this.ContactPerson = Convert.ToString(reader["ContactPerson"]);
            this.CompPhone = Convert.ToString(reader["CompPhone"]);
            this.IsActive = Convert.ToBoolean(reader["CompActive"]);
            this.fk_CountryID = Convert.ToInt32(reader["fk_CountryID"]);
            if (ColumnExists(reader, "fk_StateID"))
            {
                if (reader["fk_StateID"] != DBNull.Value)
                    this.fk_StateID = Convert.ToInt32(reader["fk_StateID"]);
            }
            if (ColumnExists(reader, "Country"))
            {
                if (reader["Country"] != DBNull.Value)
                    this.Country = Convert.ToString(reader["Country"]);
            }
            if (ColumnExists(reader, "StateName"))
            {
                if (reader["StateName"] != DBNull.Value)
                    this.StateName = Convert.ToString(reader["StateName"]);
            }

            if (ColumnExists(reader, "City"))
            {
                if (reader["City"] != DBNull.Value)
                    this.StateName = Convert.ToString(reader["City"]);
            }

            if (ColumnExists(reader, "PIN"))
            {
                if (reader["PIN"] != DBNull.Value)
                    this.StateName = Convert.ToString(reader["PIN"]);
            }

        }

        #endregion

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }


}
