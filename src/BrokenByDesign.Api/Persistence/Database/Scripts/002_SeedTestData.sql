INSERT INTO users (id, username, email, password_hash)
    VALUES
        ('00000000-0000-0000-0000-000000000001', 'alice.morgan', 'alice.morgan@example.com', 'hashedpassword1'),
        ('00000000-0000-0000-0000-000000000002', 'bruno.martin', 'bruno.martin@example.com', 'hashedpassword2'),
        ('00000000-0000-0000-0000-000000000003', 'clara.lopez', 'clara.lopez@example.com', 'hashedpassword3'),
        ('00000000-0000-0000-0000-000000000004', 'david.nguyen', 'david.nguyen@example.com', 'hashedpassword4'),
        ('00000000-0000-0000-0000-000000000005', 'elena.kowalski', 'elena.kowalski@example.com', 'hashedpassword5');
INSERT INTO groups (id, name, description)
    VALUES
        ('00000000-0000-0000-0000-000000000101', 'Tech Enthusiasts', 'A group for people passionate about tech and software.'),
        ('00000000-0000-0000-0000-000000000102', 'Green Future', 'Focused on sustainability, climate, and eco-friendly innovation.');

INSERT INTO memberships (user_id, group_id, role)
    VALUES
        -- Tech Enthusiasts
        ('00000000-0000-0000-0000-000000000001', '00000000-0000-0000-0000-000000000101', 'Organizer'), -- Alice
        ('00000000-0000-0000-0000-000000000002', '00000000-0000-0000-0000-000000000101', 'Member'),    -- Bruno
        ('00000000-0000-0000-0000-000000000003', '00000000-0000-0000-0000-000000000101', 'Admin'),     -- Clara

        -- Green Future
        ('00000000-0000-0000-0000-000000000004', '00000000-0000-0000-0000-000000000102', 'Organizer'), -- David
        ('00000000-0000-0000-0000-000000000005', '00000000-0000-0000-0000-000000000102', 'Member');    -- Elena

INSERT INTO events (id, group_id, title, description, location, start_time, end_time, created_by_user_id)
    VALUES
        -- Tech Enthusiasts
        ('00000000-0000-0000-0000-000000000201', 
        '00000000-0000-0000-0000-000000000101',
        'Tech Meetup: AI in 2025',
        'A discussion on recent developments and future of AI.',
        'Berlin, Germany',
        '2025-07-10T18:00:00Z', '2025-07-10T20:00:00Z',
        '00000000-0000-0000-0000-000000000001'),

        ('00000000-0000-0000-0000-000000000202', 
        '00000000-0000-0000-0000-000000000101',
        'Hackathon Weekend',
        'Two days of building cool projects together!',
        'Amsterdam, Netherlands',
        '2025-08-01T09:00:00Z', '2025-08-03T17:00:00Z',
        '00000000-0000-0000-0000-000000000003'),

        -- Green Future
        ('00000000-0000-0000-0000-000000000203', 
        '00000000-0000-0000-0000-000000000102',
        'GreenTech Expo',
        'Showcasing innovations in sustainable technology.',
        'Brussels, Belgium',
        '2025-09-12T10:00:00Z', '2025-09-12T16:00:00Z',
        '00000000-0000-0000-0000-000000000004');

INSERT INTO rsvps (user_id, event_id, status)
    VALUES
        -- AI Meetup
        ('00000000-0000-0000-0000-000000000001', '00000000-0000-0000-0000-000000000201', 'Yes'),   -- Alice
        ('00000000-0000-0000-0000-000000000002', '00000000-0000-0000-0000-000000000201', 'Maybe'), -- Bruno
        ('00000000-0000-0000-0000-000000000003', '00000000-0000-0000-0000-000000000201', 'Yes'),   -- Clara

        -- Hackathon
        ('00000000-0000-0000-0000-000000000001', '00000000-0000-0000-0000-000000000202', 'Yes'), -- Alice
        ('00000000-0000-0000-0000-000000000002', '00000000-0000-0000-0000-000000000202', 'No'),  -- Bruno

        -- GreenTech Expo
        ('00000000-0000-0000-0000-000000000004', '00000000-0000-0000-0000-000000000203', 'Yes'), -- David
        ('00000000-0000-0000-0000-000000000005', '00000000-0000-0000-0000-000000000203', 'Yes'); -- Elena