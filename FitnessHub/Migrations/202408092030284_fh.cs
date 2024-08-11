namespace FitnessHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fh : DbMigration
    {
        public override void Up()
        {
            // Drop existing foreign key and index for UserID in Bookings table
            DropForeignKey("dbo.Bookings", "UserID", "dbo.Users");
            DropIndex("dbo.Bookings", new[] { "UserID" });

            // Create Payments table
            CreateTable(
                "dbo.Payments",
                c => new
                {
                    PaymentID = c.Int(nullable: false, identity: true),
                    BookingID = c.Int(nullable: false),
                    UserID = c.String(maxLength: 128),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PaymentDate = c.DateTime(nullable: false),
                    PaymentMethod = c.String(),
                })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.BookingID)
                .Index(t => t.UserID);

            // Add FullName column to AspNetUsers table
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());

            // Alter UserID column in Bookings table to reference AspNetUsers
            AlterColumn("dbo.Bookings", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "UserID");
            AddForeignKey("dbo.Bookings", "UserID", "dbo.AspNetUsers", "Id");

            // Drop the old Users table
            DropTable("dbo.Users");
        }

        public override void Down()
        {
            // Recreate the old Users table
            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    Password = c.String(),
                    Email = c.String(),
                    Role = c.String(),
                })
                .PrimaryKey(t => t.UserID);

            // Drop new foreign keys and indexes
            DropForeignKey("dbo.Bookings", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "BookingID", "dbo.Bookings");
            DropIndex("dbo.Payments", new[] { "UserID" });
            DropIndex("dbo.Payments", new[] { "BookingID" });
            DropIndex("dbo.Bookings", new[] { "UserID" });

            // Alter UserID column in Bookings table to reference Users
            AlterColumn("dbo.Bookings", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "FullName");

            // Drop the Payments table
            DropTable("dbo.Payments");

            // Recreate index and foreign key for UserID in Bookings table
            CreateIndex("dbo.Bookings", "UserID");
            AddForeignKey("dbo.Bookings", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}