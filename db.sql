--
-- PostgreSQL database dump
--

-- Dumped from database version 15.12 (Debian 15.12-0+deb12u2)
-- Dumped by pg_dump version 15.12 (Debian 15.12-0+deb12u2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: dbo; Type: SCHEMA; Schema: -; Owner: KhasanovAM
--

CREATE SCHEMA dbo;


ALTER SCHEMA dbo OWNER TO "KhasanovAM";

--
-- Name: pgcrypto; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA dbo;


--
-- Name: EXTENSION pgcrypto; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: challenges; Type: TABLE; Schema: dbo; Owner: KhasanovAM
--

CREATE TABLE dbo.challenges (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    description text,
    start_date date NOT NULL,
    end_date date NOT NULL
);


ALTER TABLE dbo.challenges OWNER TO "KhasanovAM";

--
-- Name: challenges_id_seq; Type: SEQUENCE; Schema: dbo; Owner: KhasanovAM
--

CREATE SEQUENCE dbo.challenges_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo.challenges_id_seq OWNER TO "KhasanovAM";

--
-- Name: challenges_id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: KhasanovAM
--

ALTER SEQUENCE dbo.challenges_id_seq OWNED BY dbo.challenges.id;


--
-- Name: nutrition; Type: TABLE; Schema: dbo; Owner: KhasanovAM
--

CREATE TABLE dbo.nutrition (
    id integer NOT NULL,
    user_id integer,
    date date NOT NULL,
    calories integer,
    protein double precision,
    carbs double precision,
    fats double precision
);


ALTER TABLE dbo.nutrition OWNER TO "KhasanovAM";

--
-- Name: nutrition_id_seq; Type: SEQUENCE; Schema: dbo; Owner: KhasanovAM
--

CREATE SEQUENCE dbo.nutrition_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo.nutrition_id_seq OWNER TO "KhasanovAM";

--
-- Name: nutrition_id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: KhasanovAM
--

ALTER SEQUENCE dbo.nutrition_id_seq OWNED BY dbo.nutrition.id;


--
-- Name: sports; Type: TABLE; Schema: dbo; Owner: KhasanovAM
--

CREATE TABLE dbo.sports (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE dbo.sports OWNER TO "KhasanovAM";

--
-- Name: sports_id_seq; Type: SEQUENCE; Schema: dbo; Owner: KhasanovAM
--

CREATE SEQUENCE dbo.sports_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo.sports_id_seq OWNER TO "KhasanovAM";

--
-- Name: sports_id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: KhasanovAM
--

ALTER SEQUENCE dbo.sports_id_seq OWNED BY dbo.sports.id;


--
-- Name: users; Type: TABLE; Schema: dbo; Owner: KhasanovAM
--

CREATE TABLE dbo.users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    email character varying(100) NOT NULL,
    password_hash character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE dbo.users OWNER TO "KhasanovAM";

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: dbo; Owner: KhasanovAM
--

CREATE SEQUENCE dbo.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo.users_id_seq OWNER TO "KhasanovAM";

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: KhasanovAM
--

ALTER SEQUENCE dbo.users_id_seq OWNED BY dbo.users.id;


--
-- Name: workouts; Type: TABLE; Schema: dbo; Owner: KhasanovAM
--

CREATE TABLE dbo.workouts (
    id integer NOT NULL,
    user_id integer,
    sport_id integer,
    date date NOT NULL,
    duration integer NOT NULL,
    intensity character varying(20),
    notes text
);


ALTER TABLE dbo.workouts OWNER TO "KhasanovAM";

--
-- Name: workouts_id_seq; Type: SEQUENCE; Schema: dbo; Owner: KhasanovAM
--

CREATE SEQUENCE dbo.workouts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dbo.workouts_id_seq OWNER TO "KhasanovAM";

--
-- Name: workouts_id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: KhasanovAM
--

ALTER SEQUENCE dbo.workouts_id_seq OWNED BY dbo.workouts.id;


--
-- Name: challenges id; Type: DEFAULT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.challenges ALTER COLUMN id SET DEFAULT nextval('dbo.challenges_id_seq'::regclass);


--
-- Name: nutrition id; Type: DEFAULT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.nutrition ALTER COLUMN id SET DEFAULT nextval('dbo.nutrition_id_seq'::regclass);


--
-- Name: sports id; Type: DEFAULT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.sports ALTER COLUMN id SET DEFAULT nextval('dbo.sports_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.users ALTER COLUMN id SET DEFAULT nextval('dbo.users_id_seq'::regclass);


--
-- Name: workouts id; Type: DEFAULT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.workouts ALTER COLUMN id SET DEFAULT nextval('dbo.workouts_id_seq'::regclass);


--
-- Name: challenges challenges_pkey; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.challenges
    ADD CONSTRAINT challenges_pkey PRIMARY KEY (id);


--
-- Name: nutrition nutrition_pkey; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.nutrition
    ADD CONSTRAINT nutrition_pkey PRIMARY KEY (id);


--
-- Name: sports sports_pkey; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.sports
    ADD CONSTRAINT sports_pkey PRIMARY KEY (id);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: users users_username_key; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- Name: workouts workouts_pkey; Type: CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.workouts
    ADD CONSTRAINT workouts_pkey PRIMARY KEY (id);


--
-- Name: nutrition nutrition_user_id_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.nutrition
    ADD CONSTRAINT nutrition_user_id_fkey FOREIGN KEY (user_id) REFERENCES dbo.users(id) ON DELETE CASCADE;


--
-- Name: workouts workouts_sport_id_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.workouts
    ADD CONSTRAINT workouts_sport_id_fkey FOREIGN KEY (sport_id) REFERENCES dbo.sports(id) ON DELETE CASCADE;


--
-- Name: workouts workouts_user_id_fkey; Type: FK CONSTRAINT; Schema: dbo; Owner: KhasanovAM
--

ALTER TABLE ONLY dbo.workouts
    ADD CONSTRAINT workouts_user_id_fkey FOREIGN KEY (user_id) REFERENCES dbo.users(id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

