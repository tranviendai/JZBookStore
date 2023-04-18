using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(24)]
        public string FullName { get; set; }
        [StringLength(40)]
        public string Address { get; set; }
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public ICollection<Bill> Bills { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class KhachHang : IdentityDbContext<ApplicationUser>
    {
   
        public KhachHang()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Information_User", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("User_Authorization", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Login_Management", "dbo");

            modelBuilder.Entity<Book>();
            modelBuilder.Entity<Publisher>();
            modelBuilder.Entity<Bill>();
            modelBuilder.Entity<Author>();
            modelBuilder.Entity<Subject>();
            modelBuilder.Entity<DetailsOrder>();

        }
            public static KhachHang Create()
        {

            return new KhachHang();
        }
        public System.Data.Entity.DbSet<BookStore.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<BookStore.Models.Bill> Bills { get; set; }
        public System.Data.Entity.DbSet<BookStore.Models.Publisher> Publishers { get; set; }
        public System.Data.Entity.DbSet<BookStore.Models.Author> Authors { get; set; }
        public System.Data.Entity.DbSet<BookStore.Models.Subject> Subjects { get; set; }
        public System.Data.Entity.DbSet<BookStore.Models.DetailsOrder> DetailsOrders { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }
    }
}

