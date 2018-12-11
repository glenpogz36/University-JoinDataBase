-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Dec 12, 2018 at 12:24 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Database: `university`
--

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`id`, `name`, `description`) VALUES
(1, 'Biology', 'bio69'),
(3, 'Biology', 'bio69'),
(4, 'Biology', 'bio69'),
(5, 'Biology', 'bio69');

-- --------------------------------------------------------

--
-- Table structure for table `courses_students`
--

CREATE TABLE `courses_students` (
  `id` int(11) NOT NULL,
  `course_id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses_students`
--

INSERT INTO `courses_students` (`id`, `course_id`, `student_id`) VALUES
(1, 1, 31);

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `enrollmentDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`id`, `name`, `enrollmentDate`) VALUES
(19, 'manasa', '2012-12-31'),
(20, 'manasa', '2012-12-31'),
(21, 'manasa', '2012-12-31'),
(22, 'manasa', '2012-12-31'),
(23, 'manasa', '2012-12-31'),
(24, 'manasa', '2012-12-31'),
(25, 'manasa', '2012-12-31'),
(26, 'manasa', '2012-12-31'),
(27, 'manasa', '2012-12-31'),
(28, 'manasa', '2012-12-31'),
(29, 'manasa', '2012-12-31'),
(30, 'manasa', '2012-12-31'),
(31, 'james', '2222-12-23'),
(32, 'idhqiqwd', '1234-12-12'),
(33, 'frankenstein', '2000-01-01'),
(34, 'james', '1111-01-01');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `courses_students`
--
ALTER TABLE `courses_students`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `courses_students`
--
ALTER TABLE `courses_students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;
--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;