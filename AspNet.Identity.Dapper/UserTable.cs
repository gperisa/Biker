using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspNet.Identity.Dapper
{
    /// <summary>
    /// Class that represents the Users table in the Database
    /// </summary>
    public class UserTable<TUser> where TUser : IdentityMember
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns the AspNetUsers's name given a AspNetUsers ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserName(int id)
        {
            return db.Connection.ExecuteScalar<string>("SELECT Email FROM AspNetUsers WHERE ID = @ID", new { ID = id });
        }

        /// <summary>
        /// Returns a AspNetUsers ID given a AspNetUsers name
        /// </summary>
        /// <param name="email">The AspNetUsers's email</param>
        /// <returns></returns>
        public int GetAspNetUsersId(string email)
        {
            return db.Connection.ExecuteScalar<int>("SELECT ID FROM AspNetUsers WHERE Email = @Email", new { Email = email });
        }

        /// <summary>
        /// Returns an TUser given the AspNetUsers's ID
        /// </summary>
        /// <param name="id">The AspNetUsers's ID</param>
        /// <returns></returns>
        public TUser GetUserById(int id)
        {
            return db.Connection.Query<TUser>("SELECT * FROM AspNetUsers WHERE ID = @ID", new { ID = id })
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of TUser instances given a AspNetUsers name
        /// </summary>
        /// <param name="email">AspNetUsers's email</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string email)
        {
            return db.Connection.Query<TUser>("SELECT * FROM AspNetUsers WHERE Email = @Email", new { Email = email }).ToList();
        }

        public List<TUser> GetUserByEmail(string email)
        {
            return null;
        }

        /// <summary>
        /// Return the AspNetUsers's password hash
        /// </summary>
        /// <param name="id">The AspNetUsers's ID</param>
        /// <returns></returns>
        public string GetPasswordHash(int id)
        {
            return db.Connection.ExecuteScalar<string>("SELECT PasswordHash FROM AspNetUsers WHERE ID = @ID", new { ID = id });
        }

        /// <summary>
        /// Sets the AspNetUsers's password hash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public void SetPasswordHash(int id, string passwordHash)
        {
            db.Connection.Execute(@"UPDATE AspNetUsers SET PasswordHash = @pwdHash WHERE ID = @ID", new { pwdHash = passwordHash, ID = id });
        }

        /// <summary>
        /// Returns the AspNetUsers's security stamp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSecurityStamp(int id)
        {
            return db.Connection.ExecuteScalar<string>("SELECT SecurityStamp FROM AspNetUsers WHERE ID = @ID", new { ID = id });
        }

        /// <summary>
        /// Inserts a new AspNetUsers in the Users table
        /// </summary>
        /// <param name="AspNetUsers"></param>
        /// <returns></returns>
        public void Insert(TUser AspNetUsers)
        {
            var ID = db.Connection.ExecuteScalar<int>(@"INSERT INTO AspNetUsers
                            (UserName, PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
                            VALUES  (@name, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)
                            SELECT Cast(SCOPE_IDENTITY() as bigint)",
                            new
                            {
                                name = Regex.Match(AspNetUsers.UserName, "^.*?(?=@)").Value,
                                pwdHash = AspNetUsers.PasswordHash,
                                SecStamp = AspNetUsers.SecurityStamp,
                                email = AspNetUsers.Email,
                                emailconfirmed = AspNetUsers.EmailConfirmed,
                                phonenumber = AspNetUsers.PhoneNumber,
                                phonenumberconfirmed = AspNetUsers.PhoneNumberConfirmed,
                                accesscount = AspNetUsers.AccessFailedCount,
                                lockoutenabled = AspNetUsers.LockoutEnabled,
                                lockoutenddate = AspNetUsers.LockoutEndDateUtc,
                                twofactorenabled = AspNetUsers.TwoFactorEnabled
                            });

            // we need to set the ID to the returned IDentity generated from the db
            AspNetUsers.Id = ID;
        }

        /// <summary>
        /// Deletes a AspNetUsers from the Users table
        /// </summary>
        /// <param name="id">The AspNetUsers's ID</param>
        /// <returns></returns>
        private void Delete(int id)
        {
            db.Connection.Execute(@"DELETE FROM AspNetUsers WHERE ID = @ID", new { ID = id });
        }

        /// <summary>
        /// Deletes a AspNetUsers from the Users table
        /// </summary>
        /// <param name="AspNetUsers"></param>
        /// <returns></returns>
        public void Delete(TUser AspNetUsers)
        {
            Delete(AspNetUsers.Id);
        }

        /// <summary>
        /// Updates a AspNetUsers in the Users table
        /// </summary>
        /// <param name="AspNetUsers"></param>
        /// <returns></returns>
        public void Update(TUser AspNetUsers)
        {
            db.Connection
              .Execute(@"UPDATE AspNetUsers SET UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
                WHERE ID = @ID",
                new
                {
                    //userName = AspNetUsers.UserName,
                    pswHash = AspNetUsers.PasswordHash,
                    secStamp = AspNetUsers.SecurityStamp,
                    ID = AspNetUsers.Id,
                    email = AspNetUsers.Email,
                    emailconfirmed = AspNetUsers.EmailConfirmed,
                    phonenumber = AspNetUsers.PhoneNumber,
                    phonenumberconfirmed = AspNetUsers.PhoneNumberConfirmed,
                    accesscount = AspNetUsers.AccessFailedCount,
                    lockoutenabled = AspNetUsers.LockoutEnabled,
                    lockoutenddate = AspNetUsers.LockoutEndDateUtc,
                    twofactorenabled = AspNetUsers.TwoFactorEnabled
                }
           );
        }
    }
}
