﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="bms.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="home1.css">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Header -->
        <section id="header">
            <div class="header container">
                <div class="nav-bar">
                    <div class="branded">
                        <a href="#home">
                            <h2>BMS</h2>
                            <img src="images/bmslogo.jpg">
                        </a>
                    </div>
                    <div class="nav-list">
                        <div class="hamburger">
                            <div class="bar"></div>
                        </div>
                        <ul>
                            <li><a href="#home" data-after="Home">Home</a></li>
                            <li><a href="#services" data-after="Service">Services</a></li>
                            <li><a href="#projects" data-after="Projects">Projects</a></li>
                            <li><a href="#about" data-after="About">About</a></li>
                            <li><a href="#contact" data-after="Contact">Contact</a></li>
                            <li class="dropdown">
                                <a class="dropbtn">Login</a>
                                <div class="dropdown-content">
                                    <asp:LinkButton ID="AdminLoginLink" runat="server" OnClick="AdminLogin_Click">Admin Login</asp:LinkButton>
                                    <asp:LinkButton ID="ResidentLoginLink" runat="server" OnClick="ResidentLogin_Click">Resident Login</asp:LinkButton>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Header -->

        <!-- Hero Section  -->
        <section id="home">
            <div class="home container">
                <div>
                    <h1>Welcome, <span></span></h1>
                    <h1>To Barangay <span></span></h1>
                    <h1>Narvacan II <span></span></h1>
                    <a href="#projects" type="button" class="cta">Projects</a>
                </div>
            </div>
        </section>
        <!-- End Hero Section  -->

        <!-- Service Section -->
        <section id="services">
            <div class="services container">
                <div class="service-top">
                    <h1 class="section-title">Serv<span>i</span>ces</h1>
                    <p>Barangay Management System (BMS) is a comprehensive solution designed to streamline and enhance the administrative functions of barangay offices. It provides a centralized platform for efficient management of blotter records and cases, resident information, and issuance of various certificates. The system ensures accurate record-keeping, simplifies the process of certificate issuance, and improves overall service delivery to residents. With its user-friendly interface and robust features, BMS empowers barangay officials to handle administrative tasks more effectively and foster better community engagement.</p>
                </div>
                <div class="service-bottom">
                    <div class="service-item">
                        <i class="fas fa-user-friends" style="font-size: 45px; color: white; margin-left: 110px; margin-bottom: 20px"></i>
                        <h2>Resident's Information</h2>
                        <p>Maintain an up-to-date database of all residents. Store and manage detailed personal information for each resident. Facilitate quick access to resident records for administrative purposes.</p>
                    </div>
                    <div class="service-item">
                        <i class="fas fa-layer-group" style="font-size: 45px; color: white; margin-left: 110px; margin-bottom: 20px"></i>
                        <h2 style="margin-left: 90px;">Blotter</h2>
                        <p>Efficiently record and track blotter entries. Manage case details, statuses, and resolutions. Generate reports and maintain a comprehensive log of incidents within the barangay.</p>
                    </div>
                    <div class="service-item">
                        <i class="fas fa-folder" style="font-size: 45px; color: white; margin-left: 110px; margin-bottom: 20px"></i>
                        <h2>Issuance of Certificate</h2>
                        <p>Streamline the process of issuing various certificates such as Barangay Clearance, Certificate of Residency, and Certificate of Indigency. Allow residents to request certificates online. Track the status of certificate requests and manage the issuance process efficiently.</p>
                    </div>                    
                </div>
            </div>
        </section>
        <!-- End Service Section -->

        <!-- Projects Section -->
        <section id="projects">
            <div class="projects container">
                <div class="projects-header">
                    <h1 class="section-title">Recent <span>Projects</span></h1>
                </div>
                <div class="all-projects">
                    <div class="project-item">
                        <div class="project-info">
                            <h1>Project 1</h1>
                            <h2>Barangay Clean Up Drive Project</h2>
                            <p>Community based initiative that seeks to preserve the barangay’s cleanliness and aesthetic appeal through routine waste disposal, garbage collection, and environmental awareness campaigns in which the local populace is actively involved.</p>
                        </div>
                        <div class="project-img">
                            <img src="https://raw.githubusercontent.com/kimmyahhhh/BMS/c74d6df6f1877af80d9177999eb10173d7d5ec97/cud%20.jpg" alt="img">
                        </div>
                    </div>
                    <div class="project-item">
                        <div class="project-info">
                            <h1>Project 2</h1>
                            <h2>Barangay Waste Management Project</h2>
                            <p>and a cleaner, healthier community, the Barangay Waste Management Project aims to build Effective and sustainable garbage collection, segregation, and disposal systems within the barangay.</p>
                        </div>
                        <div class="project-img">
                            <img src="https://raw.githubusercontent.com/kimmyahhhh/BMS/c74d6df6f1877af80d9177999eb10173d7d5ec97/wmp%20.JPG" alt="img">
                        </div>
                    </div>
                    <div class="project-item">
                        <div class="project-info">
                            <h1>Project 3</h1>
                            <h2>Barangay Food Security and Nutrition Project</h2>
                            <p>By creating community vegetable gardens, giving fruit tree seedlings to homes, and holding nutrition education workshops for moms, the Barangay Food Security and Nutrition Project seeks to enhance the community’s nutritional health and food security.</p>
                        </div>
                        <div class="project-img">
                            <img src="https://raw.githubusercontent.com/kimmyahhhh/BMS/c74d6df6f1877af80d9177999eb10173d7d5ec97/fsn%20.JPG" alt="img">
                        </div>
                    </div>
                    <div class="project-item">
                        <div class="project-info">
                            <h1>Project 4</h1>
                            <h2>Barangay Livelihood and Skills Training Project</h2>
                            <p>Provides a range of courses and programs aimed at providing community members with the necessary information and skills to start or grow small businesses and enhance their economic prospects.</p>
                        </div>
                        <div class="project-img">
                            <img src="https://raw.githubusercontent.com/kimmyahhhh/BMS/c74d6df6f1877af80d9177999eb10173d7d5ec97/llst.JPG" alt="img">
                        </div>
                    </div>
                    <div class="project-item">
                        <div class="project-info">
                            <h1>Project 5</h1>
                            <h2>Barangay Youth Development Project</h2>
                            <p>Centered on planning sports, leisure, and educational events for kids and teenager in order to promote their development as individuals, as leaders, and as members of the community.</p>
                        </div>
                        <div class="project-img">
                            <img src="https://raw.githubusercontent.com/kimmyahhhh/BMS/c74d6df6f1877af80d9177999eb10173d7d5ec97/ydp.JPG" alt="img">
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Projects Section -->

        <!-- About Section -->
        <section id="about">
            <div class="about container">
                <div class="col-left">
                    <div class="about-img">
                        <img src="./img/img-2.png" alt="img">
                    </div>
                </div>
                <div class="col-right">
                    <h1 class="section-title">About <span>me</span></h1>
                    <p>Creating Community in the Heart of Nueva Ecija Nestled in the beautiful farmlands of Guimba, Barangay Narvacan II is a rural paradise where time-honored traditions persist. This close-knit agricultural community celebrates its deeply ingrained cultural identity by gathering for vibrant festivals and supporting one another as family. The villagers of Narvacan II rely on the fertile soil to raise abundant amounts of rice, corn, and a variety of vegetables, preserving their livelihoods through time-honored farming traditions. Though rooted in the rhythms of the countryside, Narvacan II looks forward, continually growing to fulfill the demands of its citizens while retaining the character that makes it genuinely unique.
The barangay's well-developed system of roads, canals, and public transportation connects its community to the broader municipality, facilitating the interchange of goods, services, and ideas. A sense of resilience, adaptation, and everlasting communal pride characterizes life in Barangay Narvacan II.</p>
                </div>
            </div>
        </section>
        <!-- End About Section -->

        <!-- Contact Section -->
        <section id="contact">
            <div class="contact container">
                <div>
                    <h1 class="section-title">Contact <span>info</span></h1>
                </div>
                <div class="contact-items">
                    <div class="contact-item">
                        <div class="icon"><img src="https://img.icons8.com/bubbles/100/000000/phone.png" /></div>
                        <div class="contact-info">
                            <h1>Phone</h1>
                            <h2>+1 234 123 1234</h2>
                            <h2>+1 234 123 1234</h2>
                        </div>
                    </div>
                    <div class="contact-item">
                        <div class="icon"><img src="https://img.icons8.com/bubbles/100/000000/new-post.png" /></div>
                        <div class="contact-info">
                            <h1>Email</h1>
                            <h2>bmsn2@gmail.com</h2>
                            <h2>bms@gmail.com</h2>
                        </div>
                    </div>
                    <div class="contact-item">
                        <div class="icon"><img src="https://img.icons8.com/bubbles/100/000000/map-marker.png" /></div>
                        <div class="contact-info">
                            <h1>Address</h1>
                            <h2>Narvacan II, Guimba, Nueva Ecija, Philippines</h2>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Contact Section -->

        <!-- Footer -->
        <section id="footer">
            <div class="footer container">
                <div class="brand">
                    <h1><span>B</span>MS <span>N</span>2</h1>
                </div>
                <br />
                <div class="social-icon">
                    <div class="social-item">
                        <a href="#"><img src="https://img.icons8.com/bubbles/100/000000/facebook-new.png" /></a>
                    </div>
                    <div class="social-item">
                        <a href="#"><img src="https://img.icons8.com/bubbles/100/000000/instagram-new.png" /></a>
                    </div>
                    <div class="social-item">
                        <a href="#"><img src="https://img.icons8.com/bubbles/100/000000/twitter.png" /></a>
                    </div>
                </div>
                <p>Copyright © 2024 BMS. All rights reserved</p>
            </div>
        </section>
        <!-- End Footer -->
    </form>
</body>
</html>
