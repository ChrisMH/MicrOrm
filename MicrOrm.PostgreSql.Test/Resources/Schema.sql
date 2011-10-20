CREATE SCHEMA test;

CREATE TABLE test.user
(
  id serial NOT NULL,
  name varchar NOT NULL,
  email varchar,

  CONSTRAINT pk_user PRIMARY KEY(id)
);


CREATE FUNCTION test.get_users()
RETURNS TABLE(name varchar, email varchar)
AS
$$
BEGIN
  RETURN QUERY SELECT u.name, u.email FROM test.user u;
END;
$$
LANGUAGE plpgsql;

CREATE FUNCTION test.get_users(user_names varchar[])
RETURNS TABLE(id integer, name varchar, email varchar)
AS
$$
BEGIN
  RETURN QUERY SELECT u.id, u.name, u.email FROM test.user u WHERE u.name=ANY(user_names);
END;
$$
LANGUAGE plpgsql;

CREATE FUNCTION test.get_user_id(user_name varchar)
RETURNS integer
AS
$$
DECLARE
  d_id integer;
BEGIN
  SELECT INTO d_id u.id FROM test.user u WHERE u.name = user_name LIMIT 1;
  return d_id;
END;
$$
LANGUAGE plpgsql;


