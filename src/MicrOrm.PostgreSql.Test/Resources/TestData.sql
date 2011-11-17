DELETE FROM test.user;
ALTER SEQUENCE test.user_id_seq RESTART;

INSERT INTO test.user (name, email) VALUES ('Bob', 'bob@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Fred', 'fred@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Jane', 'jane@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Ed', 'ed@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Jim', 'jim@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Gary', 'gary@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Bob', NULL);
