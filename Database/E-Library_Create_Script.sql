create table publisher(PID int AUTO_INCREMENT,
	name text Not Null,
    Primary key (PID)
    );
    
create table author(AID int AUTO_INCREMENT,
	name text Not Null,
	primary key (AID)
    );
    
create table patron(userID int AUTO_INCREMENT,
	name text Not Null,
    email text not null,
    password text not null,
    primary key (userID)
    );
    
create table authors(AuthorsID int auto_increment,
	A1 int Not Null,
    A2 int,
    A3 int,
    A4 int,
	Primary Key (AuthorsID),
    Foreign Key (A1) References author(AID),
    Foreign Key (A2) References author(AID),
    Foreign Key (A3) References author(AID),
    Foreign Key (A4) References author(AID)
    );
create table book(BID int auto_increment,
	title text not null, 
    PDF text not null,
    checkout datetime,
    checkin datetime,
    AuthorsID int not null,
    PID int not null,
    borrower int,
    primary key (BID),
    foreign key (AuthorsID) references authors(AuthorsID),
    foreign key (PID) references publisher(PID),
    foreign key (borrower) references patron(UserID)
    );