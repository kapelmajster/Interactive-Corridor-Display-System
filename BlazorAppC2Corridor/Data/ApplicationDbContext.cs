using BlazorAppC2Corridor.Enums;
using BlazorAppC2Corridor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorAppC2Corridor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SmallScreen> SmallScreen { get; set; }
        public DbSet<BigScreen> BigScreen { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<CarouselContent> CarouselContent { get; set; }
        public DbSet<UploadedFile> File { get; set; }
        public DbSet<TimetableSmallScreen> TimetableSmallScreen { get; set; }
        public DbSet<BigScreenCarousel> BigScreenCarousel { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
                optionsBuilder
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(x => System.Diagnostics.Debug.WriteLine(x));
                optionsBuilder
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TimetableSmallScreen>()
                .HasKey(ts => new { ts.TimetableId, ts.SmallScreenId });

            modelBuilder.Entity<TimetableSmallScreen>()
                .HasOne(ts => ts.Timetable)
                .WithMany(t => t.TimetableSmallScreens)
                .HasForeignKey(ts => ts.TimetableId);

            modelBuilder.Entity<TimetableSmallScreen>()
                .HasOne(ts => ts.SmallScreen)
                .WithMany(s => s.TimetableSmallScreens)
                .HasForeignKey(ts => ts.SmallScreenId);

            modelBuilder.Entity<BigScreenCarousel>()
                .HasKey(bc => new { bc.BigScreenId, bc.CarouselContentId });

            modelBuilder.Entity<BigScreenCarousel>()
                .HasOne(bc => bc.BigScreen)
                .WithMany(b => b.BigScreenCarousels)
                .HasForeignKey(bc => bc.BigScreenId);

            modelBuilder.Entity<BigScreenCarousel>()
                .HasOne(bc => bc.CarouselContent)
                .WithMany(c => c.BigScreenCarousels)
                .HasForeignKey(bc => bc.CarouselContentId);

            modelBuilder.Entity<CarouselContent>()
               .Property(e => e.ContentType)
               .HasConversion(new EnumToStringConverter<ContentType>());

            modelBuilder.Entity<SmallScreen>()
                .HasData(
                new SmallScreen { Id = 1, RoomNumber = "B1-005", RoomName = "The Lovelace Computing Lab", Bibliography = "Mathematician and writer, chiefly known for her work on Charles Babbage’s proposed mechanical general-purpose computer, the Analytical Engine, and often regarded as the first ever computer programmer." },
                new SmallScreen { Id = 2, RoomNumber = "B2-002", RoomName = "The Babbage Computing Lab", Bibliography = "Charles Babbage, a mathematician, philosopher, inventor, and mechanical engineer, considered by many as the “father” of computing, Babbage originated the concept of a digital programmable computer." },
                new SmallScreen { Id = 3, RoomNumber = "B2-003", RoomName = "The Stroustrup Computing Lab", Bibliography = "Bjarne Stroustrup, a Danish computer scientist, most notable for the creation and development of the industry-standard C++ programming language in 1985, an object-oriented general-purpose programming language that is still used today in the mainstream development of system and application programs and computer and console games on multiple hardware and operating system platforms." },
                new SmallScreen { Id = 4, RoomNumber = "B2-005", RoomName = "The Sunshine Staff Room", Bibliography = "Carl Sunshine, one of “the fathers of the Internet”, who along with Bob Kahn and Vint Cerf, first proposed the Transmission Control Protocol (TCP) and the Internet Protocol (IP), the fundamental communication protocols at the heart of the Internet. ", IsStaffRoom = true, StaffName = "Lecturers: Adam Isherwood, Celestine Iwendi, Shivang Shukla, Pradeep Hewage, Mohammed Benmubarak, Abayomi Arowosegbe, Ibtisam Mogul" },
                new SmallScreen { Id = 5, RoomNumber = "C2-001", RoomName = "The Wirth Common Room", Bibliography = "Niklaus Wirth, a Swiss computer scientist, is most known for designing several computer programming languages including ALGOL W, Pascal and Modula-2 among others." },
                new SmallScreen { Id = 6, RoomNumber = "C2-003", RoomName = "The Turing Computing Lab", Bibliography = "Alan Turing, was an English mathematician, computer scientist, logician, cryptanalyst, philosopher, and theoretical biologist. Turing was highly influential in the development of theoretical computer science, providing a formalisation of the concepts of algorithm and computation with the Turing machine, which can be considered a model of a general-purpose computer. Turing is widely considered to be the father of theoretical computer science and artificial intelligence. During the Second World War, Turing worked for the Government at Bletchley Park, Britain’s codebreaking center where he devised a number of techniques for “breaking” Germany’s top-secret Enigma code used to send military instructions to German forces, thus shortening the war by many months and saving many thousands of lives on both sides." },
                new SmallScreen { Id = 7, RoomNumber = "C2-004", RoomName = "The Berners-Lee Computing Lab", Bibliography = "Tim Berners-Lee, an English computer scientist best known as the inventor of the World Wide Web and Hypertext Transfer Protocol (HTTP) which ultimately unpins the functioning of all web browsers and apps which work “over the web”. He devised and implemented the first web browser and web server, and helped foster the WWW’s subsequent explosive development." },
                new SmallScreen { Id = 8, RoomNumber = "C2-006", RoomName = "The Hopper Computing Lab", Bibliography = "Admiral Grace Hopper, an American computer scientist and United States Navy rear admiral. One of the first programmers of the Harvard Mk I computer, she was a pioneer of computer programming who invented one of the first linkers. The first to devise the theory of machine-independent programming languages, and the FLOW-MATIC programming language she created using this theory was later extended to create COBOL, an early high-level programming language still in use today." },
                new SmallScreen { Id = 9, RoomNumber = "C2-007", RoomName = "The Codd Staff Kitchen", Bibliography = "Edgar F. Codd, an English computer scientist who, while working for IBM, invented the relational model for database management, the theoretical basis for relational databases and relational database management systems. He made other valuable contributions to computer science, but the relational model, a very influential general theory of data management, remains his most mentioned, analyzed and celebrated achievement.", IsStaffRoom = true, StaffName = "Staff Kitchen" },
                new SmallScreen { Id = 10, RoomNumber = "C2-007A", RoomName = "The Gosling Staff Room", Bibliography = "James Gosling, a Canadian computer scientist, best known as the founder and lead designer behind the Java programming language, an object-oriented platform-agnostic general-purpose programming language, used to build applications for a variety of purposes ranging from back-office bank processing systems to apps on Android mobile phones via the use of a Virtual Machine approach to execution.", IsStaffRoom = true, StaffName = "Lecturers: Anchal Garg, Thaier Hamid, Naveed Islam, Bren Tighe" },
                new SmallScreen { Id = 11, RoomNumber = "C2-008", RoomName = "The Sinclair Computing Lab", Bibliography = "Sir Clive Sinclair, an English entrepreneur and inventor, is best known for being a pioneer in the computing industry, and as the founder of several companies that developed consumer electronics in the 1970s and 1980s, the most significant being Sinclair Research which designed and launched several of the best-selling, truly affordable home computers of the 1980s. These were the ZX80, ZX81 and – most famously – the ZX Spectrum (which became the UK’s best-selling home microcomputer of all time during its 10 years in production), spawning a generation of “bedroom coders” who wrote games and kids who played them and later sought careers in computing or games design as a result. Thus Sinclair’s computers were instrumental in “kick-starting” the UK’s computer software and video games industries, which remain large and important sectors of the UK’s economy to this day." },
                new SmallScreen { Id = 12, RoomNumber = "C2-009", RoomName = "The von Neumann Project Lab", Bibliography = "John von Neumann, an Hungarian-American mathematician, physicist, computer scientist, engineer and polymath. In terms of computing science, von Neumann is notable for his work on landmark early computers during the 1940s such as EDVAC and ENIAC." },
                new SmallScreen { Id = 13, RoomNumber = "C2-010", RoomName = "The van Rossum Staff Room", Bibliography = "Guido van Rossum, a Dutch programmer, is best known as the creator of the Python programming language and has worked at a number of key players in the modern computing industry during his career, such as Google, Dropbox and Microsoft.", IsStaffRoom = true, StaffName = "Lecturers: William Haddock, Mansoor Ihsan, Andrew Parker" },
                new SmallScreen { Id = 14, RoomNumber = "C2-010A", RoomName = "The Hejlsberg Staff Room", Bibliography = "Anders Hejlsberg, a Danish software engineer who co-designed several programming languages and development tools. He was the original author and chief architect respectively of Borland Software’s Turbo Pascal and Delphi development tools and then later – having moved to Microsoft – the designer and architect of C# in 2000, an object-oriented platform-agnostic general-purpose programming language, used to build applications for a variety of purposes and platforms ranging from back-office processing systems to console games, from command-line utilities to desktop applications and from web browser applications to cross-platform native applications on mobile phones and tablets.", IsStaffRoom = true, StaffName = "Lecturers: Abdul Razak, Aamir Abbas, Andrew Holland, Helen Martin" },
                new SmallScreen { Id = 15, RoomNumber = "C2-011", RoomName = "The Kahn Staff Room", Bibliography = "Robert Elliot Kahn (born December 23, 1938), an American electrical engineer, who, along with Vint Cerf, first proposed the Transmission Control Protocol (TCP) and the Internet Protocol (IP), the fundamental communication protocols at the heart of the Internet.", IsStaffRoom = true, StaffName = "Lecturers: Francis Morrissey, Eugen Harinda" },
                new SmallScreen { Id = 16, RoomNumber = "C2-012", RoomName = "The Morris Project Lab", Bibliography = "Brian Morris, formerly a Senior Lecturer in the School of Creative Technologies. He exemplifies the University of Bolton’s belief in the widening participation agenda: he gained a first-class degree at Bolton after previously working in diverse careers such as Engineering and Greater Manchester Police. After graduation, he joined us as a lecturer. His experience led him to lead the development of our Foundation year provision to help many more students from ‘non-traditional’ backgrounds access higher education. He also played a leading role in developing the Games Design programme." },
                new SmallScreen { Id = 17, RoomNumber = "C2-013", RoomName = "The Ritchie Computing Lab", Bibliography = "Dennis Ritchie was a leading computer scientist at the famous Bell Labs in New Jersey. His notable achievements include the creation of the C programming language. C is actively used 50 years after its original release and is an ancestor of Java, C# and C++.\r\n\r\nTogether with Ken Thompson, Ritchie developed the Unix operating system using C. Unix has also survived 50 years and is in active use. It also forms the basis of Apple iOS, Android and Linux.\r\n\r\nAlong with Brian Kernighan he wrote the definitive text book “The C Programming Language” which gave the world the concept of Hello World as a starter program." },
                new SmallScreen { Id = 18, RoomNumber = "C2-017", RoomName = "The Cerf Networking Lab", Bibliography = "Vint Cert, an American Internet pioneer and recognised as one of “the fathers of the Internet”, who along with Bob Kahn and Carl Sunshine, first proposed the Transmission Control Protocol (TCP) and the Internet Protocol (IP), the fundamental communication protocols at the heart of the Internet." }
            );

            modelBuilder.Entity<Timetable>()
                .HasData(
                // b1-005 
                new Timetable
                {
                    Id = 1,
                    ModuleCode = "VFX5104",
                    ModuleName = "Advanced Digital Concepting",
                    Date = DateTime.Parse("2023-05-15"),
                    StartTime = TimeSpan.Parse("10:00"),
                    EndTime = TimeSpan.Parse("13:00"),
                    Lecturer = "George Bailey"
                },
                new Timetable
                {
                    Id = 2,
                    ModuleCode = "VFX5104",
                    ModuleName = "Advanced Digital Concepting",
                    Date = DateTime.Parse("2023-05-15"),
                    StartTime = TimeSpan.Parse("14:00"),
                    EndTime = TimeSpan.Parse("16:30"),
                    Lecturer = "George Bailey"
                },
                new Timetable
                {
                    Id = 3,
                    ModuleCode = "VFX5104",
                    ModuleName = "Advanced Digital Concepting",
                    Date = DateTime.Parse("2023-05-23"),
                    StartTime = TimeSpan.Parse("10:00"),
                    EndTime = TimeSpan.Parse("13:00"),
                    Lecturer = "Edwin Priest"
                },
                new Timetable
                {
                    Id = 4,
                    ModuleCode = "VFX5104",
                    ModuleName = "Advanced Digital Concepting",
                    Date = DateTime.Parse("2023-05-23"),
                    StartTime = TimeSpan.Parse("14:00"),
                    EndTime = TimeSpan.Parse("16:30"),
                    Lecturer = "Edwin Priest"
                },

                // b2-002 
                new Timetable
                {
                    Id = 5,
                    ModuleCode = "SWE4201",
                    ModuleName = "Introduction to Software Development",
                    Date = DateTime.Parse("2023-05-15"),
                    StartTime = TimeSpan.Parse("10:00"),
                    EndTime = TimeSpan.Parse("13:00"),
                    Lecturer = "Abdul Razak"
                },
                new Timetable
                {
                    Id = 6,
                    ModuleCode = "SWE4201",
                    ModuleName = "Introduction to Software Development",
                    Date = DateTime.Parse("2023-05-15"),
                    StartTime = TimeSpan.Parse("14:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Abdul Razak"
                },
                new Timetable
                {
                    Id = 7,
                    ModuleCode = "SWE4201",
                    ModuleName = "Introduction to Software Development",
                    Date = DateTime.Parse("2023-05-23"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Abdul Razak"
                },
                new Timetable
                {
                    Id = 8,
                    ModuleCode = "SWE4201",
                    ModuleName = "Introduction to Software Development",
                    Date = DateTime.Parse("2023-05-23"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Abdul Razak"
                },

                // b2-003 
                new Timetable
                {
                    Id = 9,
                    ModuleCode = "CTF3204",
                    ModuleName = "Foundation Project",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Mohammed Benmubarak"
                },
                new Timetable
                {
                    Id = 10,
                    ModuleCode = "CTF3204",
                    ModuleName = "Foundation Project",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Mohammed Benmubarak"
                },
                new Timetable
                {
                    Id = 11,
                    ModuleCode = "CIE5010",
                    ModuleName = "Modelling and Statistics in Environmental Science",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Furat Al-Faraj"
                },

                // c2-003 
                new Timetable
                {
                    Id = 12,
                    ModuleCode = "DAT7006",
                    ModuleName = "Data Science",
                    Date = DateTime.Parse("2023-05-18"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Anchal Garg"
                },
                new Timetable
                {
                    Id = 13,
                    ModuleCode = "DAT7006",
                    ModuleName = "Data Science",
                    Date = DateTime.Parse("2023-05-18"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Anchal Garg"
                },
                new Timetable
                {
                    Id = 14,
                    ModuleCode = "DAT7006",
                    ModuleName = "Data Science",
                    Date = DateTime.Parse("2023-05-26"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Anchal Garg"
                },

                // c2-004 
                new Timetable
                {
                    Id = 15,
                    ModuleCode = "SWE4202",
                    ModuleName = "Computing Infrastructure",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Francis Morrissey"
                },
                new Timetable
                {
                    Id = 16,
                    ModuleCode = "SWE4202",
                    ModuleName = "Computing Infrastructure",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Francis Morrissey"
                },
                new Timetable
                {
                    Id = 17,
                    ModuleCode = "SWE6204",
                    ModuleName = "Machine Learning",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Shivang Shukla"
                },
                new Timetable
                {
                    Id = 18,
                    ModuleCode = "SWE4201",
                    ModuleName = "Introduction to Software Development",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Aamir Abbas, Abdul Razak"
                },

                // c2-006 
                new Timetable
                {
                    Id = 19,
                    ModuleCode = "CIE4014",
                    ModuleName = "Introduction to Environmental Science and Engineering",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Junfeng Geng, Anna Williamson"
                },
                new Timetable
                {
                    Id = 20,
                    ModuleCode = "CIE4014",
                    ModuleName = "Introduction to Environmental Science and Engineering",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Junfeng Geng, Anna Williamson"
                },
                new Timetable
                {
                    Id = 21,
                    ModuleCode = "CIE4014",
                    ModuleName = "Introduction to Environmental Science and Engineering",
                    Date = DateTime.Parse("2023-05-26"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Junfeng Geng, Anna Williamson"
                },

                // c2-008
                new Timetable
                {
                    Id = 22,
                    ModuleCode = "CTF3201",
                    ModuleName = "Fundamentals of Programming",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Aamir Abbas"
                },
                new Timetable
                {
                    Id = 23,
                    ModuleCode = "CTF3201",
                    ModuleName = "Fundamentals of Programming",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Aamir Abbas"
                },
                new Timetable
                {
                    Id = 24,
                    ModuleCode = "SWE4202",
                    ModuleName = "Computing Infrastructure",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Mohammed Benmubarak"
                },
                new Timetable
                {
                    Id = 25,
                    ModuleCode = "SWE4202",
                    ModuleName = "Computing Infrastructure",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Mohammed Benmubarak"
                },

                // c2-012
                new Timetable
                {
                    Id = 26,
                    ModuleCode = "Self Study",
                    ModuleName = "Access To On Campus Resources",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("08:00"),
                    EndTime = TimeSpan.Parse("18:00"),
                    Lecturer = "N/A"
                },
                new Timetable
                {
                    Id = 27,
                    ModuleCode = "Self Study",
                    ModuleName = "Access To On Campus Resources",
                    Date = DateTime.Parse("2023-05-26"),
                    StartTime = TimeSpan.Parse("08:00"),
                    EndTime = TimeSpan.Parse("18:00"),
                    Lecturer = "N/A"
                },

                // c2-013
                new Timetable
                {
                    Id = 28,
                    ModuleCode = "SWE5206",
                    ModuleName = "Systems Analysis and Design",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Helen Martin, Abayomi Arowosegbe"
                },
                new Timetable
                {
                    Id = 29,
                    ModuleCode = "SWE5205",
                    ModuleName = "Modern Application",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Andrew Holland"
                },
                new Timetable
                {
                    Id = 30,
                    ModuleCode = "SWE6203",
                    ModuleName = "Enterprise System Development",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Andrew Holland"
                },
                new Timetable
                {
                    Id = 31,
                    ModuleCode = "SWE5201",
                    ModuleName = "Advanced Programming",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("16:00"),
                    Lecturer = "Andrew Holland"
                },

                // c2-017
                new Timetable
                {
                    Id = 32,
                    ModuleCode = "SEC4204",
                    ModuleName = "Modern Operating Systems",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Adam Isherwood"
                },
                new Timetable
                {
                    Id = 33,
                    ModuleCode = "SEC4204",
                    ModuleName = "Modern Operating Systems",
                    Date = DateTime.Parse("2023-05-16"),
                    StartTime = TimeSpan.Parse("13:00"),
                    EndTime = TimeSpan.Parse("15:00"),
                    Lecturer = "Adam Isherwood"
                },
                new Timetable
                {
                    Id = 34,
                    ModuleCode = "SEC6202",
                    ModuleName = "Ethical Hacking and Digital Forensics",
                    Date = DateTime.Parse("2023-05-25"),
                    StartTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("12:00"),
                    Lecturer = "Thaier Hamid"
                }

            );

            modelBuilder.Entity<TimetableSmallScreen>()
                .HasData(
                // b1-005
                new TimetableSmallScreen { TimetableId = 1, SmallScreenId = 1 },
                new TimetableSmallScreen { TimetableId = 2, SmallScreenId = 1 },
                new TimetableSmallScreen { TimetableId = 3, SmallScreenId = 1 },
                new TimetableSmallScreen { TimetableId = 4, SmallScreenId = 1 },

                // b2-002
                new TimetableSmallScreen { TimetableId = 5, SmallScreenId = 2 },
                new TimetableSmallScreen { TimetableId = 6, SmallScreenId = 2 },
                new TimetableSmallScreen { TimetableId = 7, SmallScreenId = 2 },
                new TimetableSmallScreen { TimetableId = 8, SmallScreenId = 2 },

                // b2-003
                new TimetableSmallScreen { TimetableId = 9, SmallScreenId = 3 },
                new TimetableSmallScreen { TimetableId = 10, SmallScreenId = 3 },
                new TimetableSmallScreen { TimetableId = 11, SmallScreenId = 3 },

                // c2-003
                new TimetableSmallScreen { TimetableId = 12, SmallScreenId = 6 },
                new TimetableSmallScreen { TimetableId = 13, SmallScreenId = 6 },
                new TimetableSmallScreen { TimetableId = 14, SmallScreenId = 6 },

                // c2-004
                new TimetableSmallScreen { TimetableId = 15, SmallScreenId = 7 },
                new TimetableSmallScreen { TimetableId = 16, SmallScreenId = 7 },
                new TimetableSmallScreen { TimetableId = 17, SmallScreenId = 7 },
                new TimetableSmallScreen { TimetableId = 18, SmallScreenId = 7 },

                // c2-006
                new TimetableSmallScreen { TimetableId = 19, SmallScreenId = 8 },
                new TimetableSmallScreen { TimetableId = 20, SmallScreenId = 8 },
                new TimetableSmallScreen { TimetableId = 21, SmallScreenId = 8 },

                // c2-008
                new TimetableSmallScreen { TimetableId = 22, SmallScreenId = 11 },
                new TimetableSmallScreen { TimetableId = 23, SmallScreenId = 11 },
                new TimetableSmallScreen { TimetableId = 24, SmallScreenId = 11 },
                new TimetableSmallScreen { TimetableId = 25, SmallScreenId = 11 },

                // c2-012
                new TimetableSmallScreen { TimetableId = 26, SmallScreenId = 16 },
                new TimetableSmallScreen { TimetableId = 27, SmallScreenId = 16 },

                // c2-013
                new TimetableSmallScreen { TimetableId = 28, SmallScreenId = 17 },
                new TimetableSmallScreen { TimetableId = 29, SmallScreenId = 17 },
                new TimetableSmallScreen { TimetableId = 30, SmallScreenId = 17 },
                new TimetableSmallScreen { TimetableId = 31, SmallScreenId = 17 },

                // c2-017
                new TimetableSmallScreen { TimetableId = 32, SmallScreenId = 18 },
                new TimetableSmallScreen { TimetableId = 33, SmallScreenId = 18 },
                new TimetableSmallScreen { TimetableId = 34, SmallScreenId = 18 }
           );

            modelBuilder.Entity<BigScreen>()
                .HasData(
                new BigScreen { Id = 1, Location = "Main Entrance" },
                new BigScreen { Id = 2, Location = "Student Room" }
            );

            // Seed media
            modelBuilder.Entity<UploadedFile>()
                .HasData(
                new UploadedFile
                {
                    Id = 1,
                    Data = System.IO.File.ReadAllBytes("wwwroot/Uploads/gubeg-12.jpg"),
                    Name = "gubeg-12.jpg",
                    ContentType = "image/jpeg"
                },
                new UploadedFile
                {
                    Id = 2,
                    Data = System.IO.File.ReadAllBytes("wwwroot/Uploads/heh.mp4"),
                    Name = "heh.mp4",
                    ContentType = "video/mp4"
                }
            );

            // Seed content with image 
            modelBuilder.Entity<CarouselContent>()
                .HasData(
                new CarouselContent
                {
                    Id = 1,
                    ContentType = ContentType.image,
                    ContentName = "Gubeg",
                    Priority = null,
                    Duration = TimeSpan.FromSeconds(15),
                    EmbargoDate = null,
                    ExpiryDate = null,
                    FileId = 1,
                    TransitionType = Transition.Fade,
                    TextContent = null
                },
            // Seed content with video 
                new CarouselContent
                {
                    Id = 2,
                    ContentType = ContentType.video,
                    ContentName = "heh heh heh heh heh another hehheh",
                    Priority = null,
                    Duration = TimeSpan.FromSeconds(8),
                    EmbargoDate = null,
                    ExpiryDate = null,
                    FileId = 2,
                    TransitionType = Transition.Fade,
                    TextContent = null
                },
            // Seed content with text 
                new CarouselContent
                {
                    Id = 3,
                    ContentType = ContentType.text,
                    ContentName = "Single Column",
                    Priority = 1,
                    Duration = TimeSpan.FromSeconds(10),
                    EmbargoDate = null,
                    ExpiryDate = null,
                    FileId = null,
                    TransitionType = Transition.Fade,
                    TextContent = "Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample. Single Column Text Sample.",
                    LayoutType = LayoutType.SingleColumn,
                    FontType = FontType.Calibri,
                    FontSize = 20
                },
                new CarouselContent
                {
                    Id = 4,
                    ContentType = ContentType.text,
                    ContentName = "Double Column",
                    Priority = 3,
                    Duration = TimeSpan.FromSeconds(12),
                    EmbargoDate = null,
                    ExpiryDate = null,
                    FileId = null,
                    TransitionType = Transition.Fade,
                    TextContent = "Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample. Double Column Text Sample.",
                    LayoutType = LayoutType.DoubleColumn,
                    FontType = FontType.Arial,
                    FontSize = 25
                },
                new CarouselContent
                {
                    Id = 5,
                    ContentType = ContentType.text,
                    ContentName = "Tripple Column",
                    Priority = 1,
                    Duration = TimeSpan.FromSeconds(16),
                    EmbargoDate = null,
                    ExpiryDate = null,
                    FileId = null,
                    TransitionType = Transition.Fade,
                    TextContent = "Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample. Tripple Column Text Sample.",
                    LayoutType = LayoutType.TripleColumn,
                    FontType = FontType.TimesNewRoman,
                    FontSize = 15
                }             
            );

            modelBuilder.Entity<BigScreenCarousel>()
            .HasData(
                new BigScreenCarousel { BigScreenId = 1, CarouselContentId = 1 },
                new BigScreenCarousel { BigScreenId = 1, CarouselContentId = 2 },
                new BigScreenCarousel { BigScreenId = 1, CarouselContentId = 3 },
                new BigScreenCarousel { BigScreenId = 1, CarouselContentId = 4 },
                new BigScreenCarousel { BigScreenId = 1, CarouselContentId = 5 },
                new BigScreenCarousel { BigScreenId = 2, CarouselContentId = 1 },
                new BigScreenCarousel { BigScreenId = 2, CarouselContentId = 2 },
                new BigScreenCarousel { BigScreenId = 2, CarouselContentId = 5 }
            );
        }
    }
}