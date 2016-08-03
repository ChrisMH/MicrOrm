DELETE FROM test.user;
DELETE FROM test.role;
DELETE FROM test.link_user_role;

ALTER SEQUENCE test.user_id_seq RESTART;
ALTER SEQUENCE test.role_id_seq RESTART;
ALTER SEQUENCE test.link_user_role_id_seq RESTART;

INSERT INTO test.user (id, name, email) VALUES (1, 'Bob', 'bob@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (2, 'Fred', 'fred@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (3, 'Jane', 'jane@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (4, 'Ed', 'ed@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (5, 'Jim', 'jim@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (6, 'Gary', 'gary@gmail.com');
INSERT INTO test.user (id, name, email) VALUES (7, 'Bob', NULL);

ALTER SEQUENCE test.user_id_seq RESTART WITH 8;

INSERT INTO test.role (id, name) VALUES (1, 'Admin');
INSERT INTO test.role (id, name) VALUES (2, 'Power');
INSERT INTO test.role (id, name) VALUES (3, 'User');
INSERT INTO test.role (id, name) VALUES (4, 'Peon');

ALTER SEQUENCE test.role_id_seq RESTART WITH 5;

INSERT INTO test.link_user_role (user_id, role_id) VALUES (1, 1);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (1, 2);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (1, 3);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (1, 4);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (2, 1);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (2, 2);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (3, 2);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (3, 3);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (4, 3);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (4, 4);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (5, 1);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (5, 3);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (6, 2);
INSERT INTO test.link_user_role (user_id, role_id) VALUES (6, 4);