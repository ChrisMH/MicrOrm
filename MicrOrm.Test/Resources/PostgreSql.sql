CREATE SCHEMA test;

CREATE TABLE test.user
(
  id serial NOT NULL,
  name varchar NOT NULL,
  email varchar
) WITH (OIDS=FALSE);

ALTER TABLE test.user
  ADD CONSTRAINT pk_user
  PRIMARY KEY (id);

INSERT INTO test.user (name, email) VALUES ('Bob', 'bob@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Fred', 'fred@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Jane', 'jane@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Ed', 'ed@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Jim', 'jim@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Gary', 'gary@gmail.com');
INSERT INTO test.user (name, email) VALUES ('Bob', NULL);

CREATE FUNCTION test.get_users()
RETURNS TABLE(name varchar, email varchar)
AS
$$
BEGIN
  RETURN QUERY SELECT u.name, u.email FROM test.user u;
END;
$$
LANGUAGE plpgsql;

CREATE FUNCTION test.get_users(user_ids integer[])
RETURNS TABLE(name varchar, email varchar)
AS
$$
BEGIN
  RETURN QUERY SELECT u.name, u.email FROM test.user u WHERE u.id=ANY(user_ids);
END;
$$
LANGUAGE plpgsql;