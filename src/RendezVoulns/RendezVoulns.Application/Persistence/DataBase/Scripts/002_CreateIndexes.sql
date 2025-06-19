CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS events_slug_active_idx ON events(slug)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS events_title_active_idx ON events(title)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS users_username_active_idx ON users(username)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS users_email_active_idx ON users(email)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS users_slug_active_idx ON users(slug)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS tags_name_active_idx ON tags(name)
WHERE deleted_on IS NULL;

CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS groups_name_active_idx ON groups(name)
WHERE deleted_on IS NULL;