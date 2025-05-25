CREATE TABLE 
    IF NOT EXISTS events (
        id UUID PRIMARY KEY NOT NULL,
        title TEXT NOT NULL,
        description TEXT NOT NULL,
        location TEXT NOT NULL,
        start_time TIMESTAMP WITHOUT TIME ZONE NOT NULL,
        end_time TIMESTAMP WITHOUT TIME ZONE NOT NULL,
        created_by_user_id UUID NOT NULL,
        created_on TIMESTAMP WITHOUT TIME ZONE NOT NULL
    );

CREATE TABLE
    IF NOT EXISTS users (
        id UUID PRIMARY KEY NOT NULL,
        username TEXT NOT NULL UNIQUE,
        email TEXT NOT NULL UNIQUE,
        password_hash TEXT NOT NULL,
        created_on TIMESTAMP WITHOUT TIME ZONE NOT NULL
    );