import sqlite3
import csv
conn = sqlite3.connect('./mysqlite.db')
cur = conn.cursor()
cur.execute('''CREATE TABLE USERS  
       (USER_ID INT PRIMARY KEY     NOT NULL,
       User_SEQID TEXT    NOT NULL,
       User_Name TEXT    NOT NULL);''')
       
reader = csv.reader(open('./users.csv', 'r'), delimiter=',')
next(reader)
for row in reader:
    to_db = [unicode(row[0], "utf8"), unicode(row[1], "utf8"), unicode(row[2], "utf8")]
    cur.execute("INSERT INTO USERS (USER_ID, User_SEQID,User_Name)VALUES (?, ?, ?);", to_db)
conn.commit()
res = cur.execute('SELECT COUNT(*) FROM USERS;')
for r in res:
    print r

res = cur.execute('SELECT * FROM USERS;')
for r in res:
    print r   
    break

cur.execute('''CREATE TABLE USERS_DETAILS
       (USER_ID INT PRIMARY KEY     NOT NULL,
       Account_Num TEXT    NOT NULL,
	   Account_Type TEXT,
       Account_Bal INT    NOT NULL,
	   CC_Num TEXT ,
	   CC_TYPE TEXT,
	   CC_Bal Int,
	   Loan_Num  TEXT,
	   Loan_Outstanding	Int
	   );''')
       
reader = csv.reader(open('./userdetails.csv', 'r'), delimiter=',')
next(reader)
for row in reader:
    to_db = [unicode(row[0], "utf8"), unicode(row[1], "utf8"), unicode(row[2], "utf8"),
	         unicode(row[3], "utf8"),unicode(row[4], "utf8"),unicode(row[5], "utf8"),
			 unicode(row[6], "utf8"),unicode(row[7], "utf8"),unicode(row[8], "utf8")]
    cur.execute("INSERT INTO USERS_DETAILS (USER_ID, Account_Num,Account_Type,Account_Bal,CC_Num,CC_TYPE,CC_Bal,Loan_Num,Loan_Outstanding)VALUES (?, ?, ?,?, ?, ?,?, ?, ?);", to_db)
conn.commit()
res = cur.execute('SELECT COUNT(*) FROM USERS_DETAILS;')
for r in res:
    print r

res = cur.execute('SELECT * FROM USERS_DETAILS;')
for r in res:
    print r   
    break

cur.execute('''CREATE TABLE SR_DETAILS
       (SR_ID INT PRIMARY KEY     NOT NULL,
       SR_SUMMARY TEXT    NOT NULL,
	   SR_Status TEXT,
       Next_Action_Date TEXT,
	   Pending_WITH TEXT ,
	   SR_Detaiils TEXT
	   );''')
       
reader = csv.reader(open('./srdetails.csv', 'r'), delimiter=',')
next(reader)
for row in reader:
    to_db = [unicode(row[0], "utf8"), unicode(row[1], "utf8"), unicode(row[2], "utf8"),
	         unicode(row[3], "utf8"),unicode(row[4], "utf8"),unicode(row[5], "utf8")]
    cur.execute("INSERT INTO SR_DETAILS (SR_ID, SR_SUMMARY,SR_Status,Next_Action_Date,Pending_WITH,SR_Detaiils)VALUES (?, ?, ?,?, ?, ?);", to_db)
conn.commit()
res = cur.execute('SELECT COUNT(*) FROM SR_DETAILS;')
for r in res:
    print r

res = cur.execute('SELECT * FROM SR_DETAILS;')
for r in res:
    print r   
    break

cur.execute('''CREATE TABLE PROD_DETAILS
       (PROD_TYPE INT NOT NULL,
       PROD_NAME TEXT    NOT NULL,
	   LITERATURE TEXT,
       CHARGES TEXT
	   );''')
       
reader = csv.reader(open('./product_info.csv', 'r'), delimiter=',')
next(reader)
for row in reader:
    to_db = [unicode(row[0], "utf8"), unicode(row[1], "utf8"), unicode(row[2], "utf8"),
	         unicode(row[3], "utf8")]
    cur.execute("INSERT INTO PROD_DETAILS (PROD_TYPE, PROD_NAME,LITERATURE,CHARGES)VALUES (?, ?, ?,?);", to_db)
conn.commit()
res = cur.execute('SELECT COUNT(*) FROM PROD_DETAILS;')
for r in res:
    print r

res = cur.execute('SELECT * FROM PROD_DETAILS;')
for r in res:
    print r   
conn.close()
