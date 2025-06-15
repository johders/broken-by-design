CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS events_slug_active_idx ON events(slug)
WHERE deleted_on IS NULL;