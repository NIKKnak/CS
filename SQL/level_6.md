USE lesson_5;

/*
WITH RECURSIVE cte AS
(
SELECT 1 AS a
UNION ALL
SELECT a + 1 FROM cte
WHERE a < 10
)
SELECT * FROM cte;
*/


CREATE DATABASE mydb;
USE mydb;

CREATE TABLE users (
username VARCHAR(50) PRIMARY KEY,
password VARCHAR(50) NOT NULL,
status VARCHAR(10) NOT NULL);

CREATE TABLE users_profile (
username VARCHAR(50) PRIMARY KEY,
name VARCHAR(50) NOT NULL,
address VARCHAR(50) NOT NULL,
email VARCHAR(50) NOT NULL,
FOREIGN KEY (username) REFERENCES users(username) ON DELETE CASCADE);

INSERT INTO users values
('admin' , '7856', 'Active'),
('staff' , '90802', 'Active'),
('manager' , '35462', 'Inactive');

INSERT INTO users_profile values
('admin', 'Administrator' , 'Dhanmondi', 'admin@test.com' ) ,
('staff', 'Jakir Nayek' , 'Mirpur', 'zakir@test.com' ),
('manager', 'Mehr Afroz' , 'Eskaton', 'mehr@test.com' );




/*
1.	��������� ���, �������� ���� ������������� �� ������� users_profile
2.	��������� ���, ����������� ���������� �������� ������������� . ������� ��������� ��������������� ����. ������:
3. 	� ������� ��� ���������� ������� ��������� ����� �� 1 �� 10:(������ ��� ����� �� 1 �� 3)
*/

-- 1
with cte1 as
(
select * from users_profile
)
select * from cte1;

-- 2
WITH cte1 AS
(
	SELECT COUNT(*) AS count FROM users
    WHERE status = 'Active'
)
SELECT * FROM cte1;

-- 3
WITH RECURSIVE cte AS
(
	SELECT 1 AS a, 1 * 1 AS res
    UNION ALL
    SELECT a + 1, POW(a + 1, 2) as res
    FROM cte
    WHERE a < 10
)
SELECT * FROM cte;

/*
������� �������, � ������� ���������� ���������� � ������������ ������������� � ������ �����,
� ����� ������� ������ ���������� ������ � ������ ����� � ����������� �� �������� � ����������
��������� ����� ���� ������
*/

select *,
max(OSZ) over(partition by TB) '������������ ������������� � ������ �����',
avg(procent_rate) over(partition by TB, SIGMENT) '������� ������ ���������� ������ � ������ ����� � ����������� �� ��������',
count(ID_DOG) over() '���������� ���������' 
from table1;


-- ������������ ������� �� �������� ���������� �������
SELECT *,
ROW_NUMBER() OVER(ORDER BY count_revisions DESC), 
RANK() OVER(ORDER BY count_revisions DESC),
DENSE_RANK() OVER(ORDER BY count_revisions DESC),
NTILE(3) OVER(ORDER BY count_revisions DESC)
FROM table1;


-- ����� ������ ����� �� ���� ������ �� ���������� �������.

WITH cte AS
(
	SELECT *, 
    DENSE_RANK() OVER(ORDER BY count_revisions DESC) AS ds
    FROM table1
)
SELECT * FROM cte
WHERE ds = 2;



SELECT *,
LEAD(Event, 1, 'end') OVER(PARTITION BY id_task ORDER BY date_event), 
LEAD(date_event, 1, '2099-01-01') OVER(PARTITION BY id_task ORDER BY date_event)
FROM table1;
