CREATE TABLE
    IF NOT EXISTS users (
        id UUID PRIMARY KEY NOT NULL,
        username TEXT NOT NULL UNIQUE,
        email TEXT NOT NULL UNIQUE,
        profile_image_url TEXT,
        created_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updated_on TIMESTAMPTZ,
        deleted_on TIMESTAMPTZ
    );

CREATE TABLE 
    IF NOT EXISTS groups (
        id UUID PRIMARY KEY NOT NULL,
        name TEXT NOT NULL,
        description TEXT NOT NULL DEFAULT '',
        created_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updated_on TIMESTAMPTZ,
        deleted_on TIMESTAMPTZ
);

CREATE TABLE 
    IF NOT EXISTS memberships (
        user_id UUID NOT NULL,
        group_id UUID NOT NULL,
        role TEXT NOT NULL DEFAULT 'Member',
        joined_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updated_on TIMESTAMPTZ,
        deleted_on TIMESTAMPTZ,
        PRIMARY KEY (user_id, group_id),
        CONSTRAINT fk_membership_user FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
        CONSTRAINT fk_membership_group FOREIGN KEY (group_id) REFERENCES groups (id) ON DELETE CASCADE,
        CONSTRAINT chk_group_role CHECK (role IN ('Member', 'Organizer', 'Admin'))
);

CREATE TABLE 
    IF NOT EXISTS events (
        id UUID PRIMARY KEY NOT NULL,
        group_id UUID NOT NULL,
        title TEXT NOT NULL,
        slug TEXT NOT NULL,
        description TEXT NOT NULL,
        location TEXT NOT NULL,
        start_time TIMESTAMPTZ NOT NULL,
        end_time TIMESTAMPTZ NOT NULL,
        created_by_user_id UUID NOT NULL,
        created_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updated_on TIMESTAMPTZ,
        deleted_on TIMESTAMPTZ,
        CONSTRAINT fk_event_group FOREIGN KEY (group_id) REFERENCES groups (id) ON DELETE CASCADE,
        CONSTRAINT fk_event_creator FOREIGN KEY (created_by_user_id) REFERENCES users (id) ON DELETE RESTRICT
);

CREATE TABLE 
    IF NOT EXISTS rsvps (
        user_id UUID NOT NULL,
        event_id UUID NOT NULL,
        status TEXT NOT NULL,
        responded_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
        updated_on TIMESTAMPTZ,
        deleted_on TIMESTAMPTZ,
        PRIMARY KEY (user_id, event_id),
        CONSTRAINT fk_rsvp_user FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE,
        CONSTRAINT fk_rsvp_event FOREIGN KEY (event_id) REFERENCES events (id) ON DELETE CASCADE,
        CONSTRAINT chk_rsvp_status CHECK (status IN ('Yes', 'No', 'Maybe'))
);

CREATE TABLE IF NOT EXISTS tags (
    id UUID PRIMARY KEY NOT NULL,
    name TEXT NOT NULL UNIQUE,
    color_hex TEXT NOT NULL DEFAULT '#cccccc',
    created_on TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMPTZ,
    deleted_on TIMESTAMPTZ
);

CREATE TABLE IF NOT EXISTS event_tags (
    event_id UUID NOT NULL,
    tag_id UUID NOT NULL,
    PRIMARY KEY (event_id, tag_id),
    CONSTRAINT fk_event FOREIGN KEY (event_id) REFERENCES events (id) ON DELETE CASCADE,
    CONSTRAINT fk_tag FOREIGN KEY (tag_id) REFERENCES tags (id) ON DELETE CASCADE
);