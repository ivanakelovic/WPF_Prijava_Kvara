-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Aug 24, 2022 at 01:46 PM
-- Server version: 10.4.10-MariaDB
-- PHP Version: 5.6.40

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tmp_ispit`
--
CREATE DATABASE IF NOT EXISTS `tmp_ispit` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `tmp_ispit`;

-- --------------------------------------------------------

--
-- Table structure for table `prijava`
--

DROP TABLE IF EXISTS `prijava`;
CREATE TABLE IF NOT EXISTS `prijava` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tip_id` int(11) NOT NULL,
  `status_id` int(11) NOT NULL DEFAULT 1,
  `opis_problema` text COLLATE utf8_unicode_ci NOT NULL,
  `lokacija` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `odgovor_sluzbe` text COLLATE utf8_unicode_ci DEFAULT NULL,
  `odgovorio` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sifra` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `datum` date DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `prijava`
--

INSERT INTO `prijava` (`id`, `tip_id`, `status_id`, `opis_problema`, `lokacija`, `odgovor_sluzbe`, `odgovorio`, `sifra`, `datum`, `created`, `modified`) VALUES
(1, 1, 4, 'Prepuni kontejneri', 'Kula', 'Dupla', 'Administrator', '200110XVRT', '2020-01-11', '2020-01-10 17:30:06', '2020-01-11 00:06:22'),
(2, 1, 2, 'Prepuni kontejneri', 'Kula', 'Obrada', 'Administrator', '20011038PR', '2020-01-11', '2020-01-10 17:32:29', '2020-01-11 00:07:33'),
(3, 6, 4, 'Parkiranje na travi', 'Mladice', 'Dupla', 'Administrator', '2001105KWY', '2020-01-11', '2020-01-10 20:30:25', '2020-01-11 00:06:46'),
(4, 6, 3, 'Parkiranje na travi', 'Mladice', 'Kaznjen pocinilac', 'Administrator', '200110M6IP', '2020-01-11', '2020-01-10 20:38:42', '2020-01-11 00:08:16'),
(5, 1, 1, 'Neredovno odvoženje', 'Spasovdanska 23', NULL, NULL, '20011135MG', '2020-01-11', '2020-01-11 00:00:24', '2020-01-11 00:00:24'),
(6, 2, 1, 'Neugodan miris kanalizacija u gradskim ulicama', 'Spasovdanska', NULL, NULL, '2001115XO5', '2020-01-11', '2020-01-11 13:27:34', '2020-01-11 13:27:34');

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
CREATE TABLE IF NOT EXISTS `status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `naziv` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `created` datetime DEFAULT NULL,
  `modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `status`
--

INSERT INTO `status` (`id`, `naziv`, `created`, `modified`) VALUES
(1, 'Podnesena', NULL, NULL),
(2, 'U obradi', NULL, NULL),
(3, 'Riješena', NULL, NULL),
(4, 'Odbačena', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tip`
--

DROP TABLE IF EXISTS `tip`;
CREATE TABLE IF NOT EXISTS `tip` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `korisnik_id` int(11) DEFAULT NULL,
  `naziv` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `created` datetime DEFAULT NULL,
  `modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `tip`
--

INSERT INTO `tip` (`id`, `korisnik_id`, `naziv`, `created`, `modified`) VALUES
(1, 1, 'Odvoženje smeća', NULL, '2020-01-05 19:16:08'),
(2, 3, 'Problemi vodovoda i kanalizacije', NULL, '2020-01-05 20:22:04'),
(3, 4, 'Ostali komunalni problemi', '2020-01-05 19:16:39', '2020-01-05 20:22:11'),
(4, 4, 'Ostalo', '2020-01-05 20:22:42', '2020-01-06 17:48:25'),
(5, 1, 'Ulična rasvjeta', '2020-01-10 16:58:06', '2020-01-10 16:58:06'),
(6, 4, 'Parkiranje na zelenim površinama', '2020-01-10 16:58:42', '2020-01-10 16:58:42');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
