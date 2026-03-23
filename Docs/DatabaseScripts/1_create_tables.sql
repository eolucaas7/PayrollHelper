CREATE TABLE IF NOT EXISTS positions
(
    id serial PRIMARY KEY,
    name character varying(50) NOT NULL UNIQUE,
    description character varying(200) NOT NULL,
    created_at timestamp without time zone DEFAULT now(),
    updated_at timestamp without time zone DEFAULT now(),
    status character varying(20) DEFAULT 'active'
);

CREATE TABLE IF NOT EXISTS taxation
(
    id serial PRIMARY KEY,
    tax_type character varying(50) NOT NULL UNIQUE,
    tax_rate numeric(5, 2) NOT NULL CHECK (tax_rate > 0 AND tax_rate <= 100),
    description character varying(255)
);

CREATE TABLE IF NOT EXISTS employees
(
    employee_id serial PRIMARY KEY,
    employee_name character varying(100) NOT NULL,
    post_number integer NOT NULL,
    insurance boolean DEFAULT false,
    phone_number character varying(15) NOT NULL UNIQUE,
    address character varying(150) NOT NULL
);

CREATE TABLE IF NOT EXISTS payments
(
    payment_id serial PRIMARY KEY,
    payment_type character varying(50) NOT NULL,
    payment_amount numeric(10, 2) NOT NULL CHECK (payment_amount > 0),
    payment_date date NOT NULL DEFAULT CURRENT_DATE,
    employee_id integer NOT NULL
);

CREATE TABLE IF NOT EXISTS salary_and_bonuses
(
    id serial PRIMARY KEY,
    payment_type character varying(50) NOT NULL UNIQUE,
    amount numeric(10, 2) NOT NULL CHECK (amount > 0),
    default_amount numeric(10, 2) CHECK (default_amount > 0),
    description character varying(255)
);

CREATE TABLE IF NOT EXISTS salary_and_bonuses_taxation
(
    salary_and_bonuses_id integer NOT NULL,
    taxation_id integer NOT NULL,
    PRIMARY KEY (salary_and_bonuses_id, taxation_id)
);