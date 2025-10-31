CREATE TABLE IF NOT EXISTS otus_ha.users
(
    id            UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    first_name    VARCHAR(30) NOT NULL,
    last_name     VARCHAR(30) NOT NULL,
    birth_date    DATE        NOT NULL,
    sex           CHAR        NOT NULL,
    biography     TEXT,
    city          VARCHAR(30),
    password_hash TEXT        NOT NULL
);

COMMENT ON TABLE otus_ha.users IS 'Пользователи';
COMMENT ON COLUMN otus_ha.users.id IS 'Идентификатор пользователя';
COMMENT ON COLUMN otus_ha.users.first_name IS 'Имя';
COMMENT ON COLUMN otus_ha.users.last_name IS 'Фамилия';
COMMENT ON COLUMN otus_ha.users.sex IS 'Пол';
COMMENT ON COLUMN otus_ha.users.birth_date IS 'Дата рождения';
COMMENT ON COLUMN otus_ha.users.biography IS 'Биография';
COMMENT ON COLUMN otus_ha.users.city IS 'Город';
COMMENT ON COLUMN otus_ha.users.password_hash IS 'Хеш пароля';