namespace FitnessHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class fitnesshub : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanceClasses",
                c => new
                {
                    ClassID = c.Int(nullable: false, identity: true),
                    StudioID = c.Int(nullable: false),
                    Name = c.String(),
                    Instructor = c.String(),
                    Schedule = c.DateTime(nullable: false),
                    Duration = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.Studios", t => t.StudioID, cascadeDelete: true)
                .Index(t => t.StudioID);

            CreateTable(
                "dbo.Studios",
                c => new
                {
                    StudioID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Location = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                })
                .PrimaryKey(t => t.StudioID);

            CreateTable(
                "dbo.SwimmingLessons",
                c => new
                {
                    LessonID = c.Int(nullable: false, identity: true),
                    PoolID = c.Int(nullable: false),
                    Name = c.String(),
                    Instructor = c.String(),
                    Schedule = c.DateTime(nullable: false),
                    Duration = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Pools", t => t.PoolID, cascadeDelete: true)
                .Index(t => t.PoolID);

            CreateTable(
                "dbo.Pools",
                c => new
                {
                    PoolID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Location = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                })
                .PrimaryKey(t => t.PoolID);

            CreateTable(
                "dbo.Bookings",
                c => new
                {
                    BookingID = c.Int(nullable: false, identity: true),
                    UserID = c.String(nullable: false), // UserID is a string for ApplicationUser
                    DanceClassID = c.Int(),
                    SwimmingLessonID = c.Int(),
                    BookingDate = c.DateTime(nullable: false),
                    AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.DanceClasses", t => t.DanceClassID)
                .ForeignKey("dbo.SwimmingLessons", t => t.SwimmingLessonID)
                .Index(t => t.UserID)
                .Index(t => t.DanceClassID)
                .Index(t => t.SwimmingLessonID);

            CreateTable(
                "dbo.Payments",
                c => new
                {
                    PaymentID = c.Int(nullable: false, identity: true),
                    BookingID = c.Int(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    PaymentDate = c.DateTime(nullable: false),
                    PaymentMethod = c.String(),
                })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .Index(t => t.BookingID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.String(nullable: false), // UserID is a string for ApplicationUser
                    Username = c.String(),
                    Password = c.String(),
                    Email = c.String(),
                    Role = c.String(),
                })
                .PrimaryKey(t => t.UserID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Payments", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "SwimmingLessonID", "dbo.SwimmingLessons");
            DropForeignKey("dbo.Bookings", "DanceClassID", "dbo.DanceClasses");
            DropForeignKey("dbo.Bookings", "UserID", "dbo.Users");
            DropForeignKey("dbo.SwimmingLessons", "PoolID", "dbo.Pools");
            DropForeignKey("dbo.DanceClasses", "StudioID", "dbo.Studios");
            DropIndex("dbo.Payments", new[] { "BookingID" });
            DropIndex("dbo.Bookings", new[] { "SwimmingLessonID" });
            DropIndex("dbo.Bookings", new[] { "DanceClassID" });
            DropIndex("dbo.Bookings", new[] { "UserID" });
            DropIndex("dbo.SwimmingLessons", new[] { "PoolID" });
            DropIndex("dbo.DanceClasses", new[] { "StudioID" });
            DropTable("dbo.Users");
            DropTable("dbo.Payments");
            DropTable("dbo.Bookings");
            DropTable("dbo.Pools");
            DropTable("dbo.SwimmingLessons");
            DropTable("dbo.Studios");
            DropTable("dbo.DanceClasses");
        }
    }
}