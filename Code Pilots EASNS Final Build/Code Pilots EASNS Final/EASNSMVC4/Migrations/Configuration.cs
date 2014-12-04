namespace EASNSMVC4.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EASNSMVC4.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EASNSMVC4.DAL.DBEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EASNSMVC4.DAL.DBEntity context)
        {
            context.Disabilities.AddOrUpdate
            (
                p => p.Desc,
                new Disability { Desc = "ADHD" },
                new Disability { Desc = "Learning Disability" },
                new Disability { Desc = "Developmental Disability" },
                new Disability { Desc = "Brain Injury" }
            );
            context.ActiveStates.AddOrUpdate
            (
                p => p.Desc,
                new ActiveState { Desc = "Awaiting Approval" },
                new ActiveState { Desc = "Approved" },
                new ActiveState { Desc = "Declined" }
            );
            context.FileTypes.AddOrUpdate
            (
                p => p.Desc,
                new FileType { Desc = "Picture" },
                new FileType { Desc = "Video" },
                new FileType { Desc = "Tutorial" },
                new FileType { Desc = "PDF" },
                new FileType { Desc = "Text Document" },
                new FileType { Desc = "e-book" }
            );
            context.Stakeholders.AddOrUpdate
            (
                p => p.Desc,
                new Stakeholder { Desc = "Agency" },
                new Stakeholder { Desc = "Vendor" },
                new Stakeholder { Desc = "Graduate" },
                new Stakeholder { Desc = "Educator" },
                new Stakeholder { Desc = "Public" }
            );
            context.TechLevels.AddOrUpdate
            (
                p => p.Desc,
                new TechLevel { Desc = "Low" },
                new TechLevel { Desc = "Medium" },
                new TechLevel { Desc = "High" }
            );

            context.Peripherals.AddOrUpdate
            (
                p => p.Desc,
                new Peripheral { Desc = "Computer", TechLevelID = 3},
                new Peripheral { Desc = "Laptop", TechLevelID = 3},
                new Peripheral { Desc = "Tablet", TechLevelID = 3}
            );

            context.Vendors.AddOrUpdate
            (
                p => p.Name,
                 new Vendor { Name = "Accent on Skills", Email = "o.schmidtca@gmail.com", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "286 Betty Ann Drive", PostalCode = "M2R 1B1" },
                new Vendor { Name = "Apple Canada", Email = "ahotton@apple.com", Country = "Canada", Province = "Ontario", City = "Markham", Address = "7495 Birchmount Road", PostalCode = "L3R 5G2", Website = "www.apple.com" },
                new Vendor { Name = "Bridges", Email = "bogdan@bridges.com", Phone = "1-800-353-1107", Country = "Canada", Province = "Ontario", City = "Mississauga", Address = "2121 Argentia Road", PostalCode = "L5N 2X", Website = "www.bridges-canada.com" },
                new Vendor { Name = "Common Senses", Email = "jslang@sympatico.ca", Phone = "905-799-3727", Country = "Canada", Province = "Ontario", City = "Brampton", Address = "71 Rosedale Avenue, Unit A8", PostalCode = "L6X 1K4", Website = "www.commonsenses.ca" },
                new Vendor { Name = "Core Learning", Email = "dhatch@core-learning.com", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "4211 Yonge Street, Suite 619", PostalCode = "M2P 2A9", Website = "www.core-learning.com" },
                new Vendor { Name = "Dynavox Technologies", Email = "danielle.franklin@dynavoxtech.com", Phone = "416-568-3342", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "3-1750 The Queensway, Suite 454", PostalCode = "M9C 5H5", Website = "www.dynavoxtech.com" },
                new Vendor { Name = "E-Instruction Canada", Email = "wayne@einstructioncanada.com", Phone = "416-693-0008", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "1450 O'Connor Drive, Bldg. No. 2, Suite 210", PostalCode = "M4B 2T8", Website = "www.einstruction.com" },
                new Vendor { Name = "Humanware", Email = "aimee.Todd@HumanWare.com", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "4141 Yonge Street, Suite 101", PostalCode = "M2P 2A8" },
                new Vendor { Name = "Louise Kool & Galt", Email = "spolak@louisekool.com", Phone = "416-293-0312", Country = "Canada", Province = "Ontario", City = "Scarbrough", Address = "1-10 Newgale Gate", PostalCode = "M1X 1C5", Website = "www.louisekool.com" },
                new Vendor { Name = "Microcomputer Science", Email = "barouchc@microscience.on.ca", Phone = "905-629-1654", Country = "Canada", Province = "Ontario", City = "Mississauga", Address = "5155 Spectrum Way, Unit 26", PostalCode = "L4W 5A1", Website = "www.microscience.on.ca" },
                new Vendor { Name = "Monarch Books", Email = "rdt.ok@rogers.com", Phone = "416-663-8231", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "5000 Dufferin Street", PostalCode = "M3H 5T5" },
                new Vendor { Name = "One Monitor", Email = "anthony@onemonitor.com", Phone = "905-669-2666", Desc = "ext. 111", Country = "Canada", Province = "Ontario", City = "Vaughan", Address = "249 Courtland Avenue", PostalCode = "L4K 4T2", Website = "www.onemonitor.com" },
                new Vendor { Name = "Phonic Ear", Email = "mb@phonicear.ca", Phone = "905-677-3231", Desc = "ext. 111", Country = "Canada", Province = "Ontario", City = "Mississauga", Address = "10-7475 Kimbel Street", PostalCode = "L5S 1E7", Website = "www.phonicear.ca" },
                new Vendor { Name = "School Specialty Canada", Email = "laddario@schoolspecialty.ca", Phone = "905-953-8300", Country = "Canada", Province = "Ontario", City = "Newmarket", Address = "238 Bristol Road", PostalCode = "L3Y 7X6", Website = "www.schoolspecialty.ca" },
                new Vendor { Name = "Strategic Transitions", Email = "sales@strategictransitions.com", Phone = "905-726-2853", Country = "Canada", Province = "Ontario", City = "Aurora", Address = "9 Industrial Pkwy South, Suites 7-8", PostalCode = "L4G 3V9", Website = "www.strategictransitions.com" },
                new Vendor { Name = "Team Classmouse", Email = "team@classmouse.com", Country = "Canada", Province = "Ontario", City = "Welland", Address = "239 Cedar Park Drive", PostalCode = "L3C 7G7" },
                new Vendor { Name = "Vocal Links", Email = "peter@vocalinks.com", Phone = "416-410-0342", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "32 Blantyre Avenue", PostalCode = "M1N 2R4", Website = "www.vocalinks.com" },
                new Vendor { Name = "Centre franco-ontarien de ressources pédagogiques", Phone = "1-877-742-3677", Desc = "ext. 287", Country = "Canada", Province = "Ontario", City = "Ottawa", Address = "435, rue Donald", PostalCode = "K1K 4X5" },
                new Vendor { Name = "Learning Disabilities Association of Ontario", Phone = "416-929-4311", Desc = "ext. 31", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "365 Bloor Street East Suite 1004", PostalCode = "M4W 3L4", Website = "www.ldao.ca" },
                new Vendor { Name = "Provincial Schools Branch, Ministry of Education", Phone = "905-878-2851", Desc = "ext. 330", Country = "Canada", Province = "Ontario", City = "Milton", Address = "255 Ontario Street South", PostalCode = "L9T 2M5" },
                new Vendor { Name = "E-Learning, Ministry of Education", Phone = "416-325-0394", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "777 Bay Street, Suite 423", PostalCode = "M5G 2E5" },
                new Vendor { Name = "OSAPAC, Ministry of Education", Email = "Beatrice.Schriever@ontario.ca", Phone = "416-325-2092", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "900 Bay Street, 16th Floor", PostalCode = "M7A 1L2" },
                new Vendor { Name = "SNOW", Email = "Karen_BrooksNelson@osapac.org", Phone = "416-978-4360", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "Adaptive Technology Resource Centre, J.P. Robarts Library, First Floor University of Toronto, 130 George Street", PostalCode = "M5S 1A5" },
                new Vendor { Name = "ASET, Ministry of Education, Provincial Schools Branch", Phone = "905-878-2851", Desc = "ext. 330", Country = "Canada", Province = "Ontario", City = "Milton", Address = "255 Ontario Street South", PostalCode = "L9T 2M5" },
                new Vendor { Name = "Communications Branch, Ministry of Education", Phone = "416-326-1463", Country = "Canada", Province = "Ontario", City = "Toronto", Address = "14th Floor, Mowat Block, 900 Bay Street", PostalCode = "M7A 1L2" }
               
            );
        }
    }
}
