import { useState } from "react";
import "@/App.css";
import { motion, AnimatePresence } from "framer-motion";
import { 
  Mail, Phone, MapPin, Linkedin, Github, ExternalLink, Download, 
  GraduationCap, BookOpen, Award, CheckCircle, User, Briefcase, 
  Code, Calendar, X, Monitor, Keyboard, Mouse, Headphones, 
  Coffee, Smartphone, HardDrive, Gamepad2, Languages, FileText
} from "lucide-react";

// Desk items representing sections
const DESK_ITEMS = {
  monitor: {
    id: "monitor",
    title: "Santiago Sánchez-Celemín",
    subtitle: "Pantalla Principal",
    icon: Monitor,
    color: "#00f3ff"
  },
  keyboard: {
    id: "keyboard", 
    title: "Habilidades",
    subtitle: "Teclado Mecánico",
    icon: Keyboard,
    color: "#ff00aa"
  },
  mouse: {
    id: "mouse",
    title: "Sobre Mí",
    subtitle: "Ratón Gaming",
    icon: Mouse,
    color: "#39ff14"
  },
  headphones: {
    id: "headphones",
    title: "Idiomas",
    subtitle: "Auriculares",
    icon: Headphones,
    color: "#ffd700"
  },
  coffee: {
    id: "coffee",
    title: "Formación",
    subtitle: "Taza de Café",
    icon: Coffee,
    color: "#ff6b35"
  },
  phone: {
    id: "phone",
    title: "Contacto",
    subtitle: "Smartphone",
    icon: Smartphone,
    color: "#00ffff"
  },
  tower: {
    id: "tower",
    title: "Proyectos",
    subtitle: "Torre PC",
    icon: HardDrive,
    color: "#b266ff"
  },
  gamepad: {
    id: "gamepad",
    title: "Cursos",
    subtitle: "Mando Gaming",
    icon: Gamepad2,
    color: "#ff69b4"
  }
};

// Content for each section
const SectionContent = ({ sectionId }) => {
  const [activeTab, setActiveTab] = useState('uni');
  
  const contents = {
    monitor: (
      <div className="monitor-content" data-testid="content-monitor">
        <div className="monitor-screen">
          {/* Profile photo area */}
          <div className="profile-photo-container">
            <div className="profile-photo">
              <img 
                src="images/Foto_Profesional.jpg" 
                alt="Santiago Sánchez-Celemín Arévalo"
                className="profile-img"
                onError={(e) => {
                  e.target.style.display = 'none';
                  e.target.nextSibling.style.display = 'flex';
                }}
              />
              <div className="profile-fallback" style={{display: 'none'}}>
                <User className="w-24 h-24 text-cyan-400" />
              </div>
            </div>
            <div className="photo-glow"></div>
          </div>
          
          <h1 className="text-3xl md:text-4xl font-bold text-cyan-400 mt-6 uppercase tracking-wider">
            Santiago Sánchez-Celemín Arévalo
          </h1>
          <p className="text-lg text-white/70 mt-2">
            Estudiante de Ingeniería Informática — UCLM
          </p>
          
          <div className="flex flex-wrap justify-center gap-3 mt-6">
            <a href="https://www.linkedin.com/in/santiago-s%C3%A1nchez-celem%C3%ADn-ar%C3%A9valo/" target="_blank" rel="noopener noreferrer" 
              className="social-btn linkedin">
              <Linkedin className="w-5 h-5" /> LinkedIn
            </a>
            <a href="https://santiagosanchez-celeminarevalo.github.io/Portfolio_SantiagoSanchez-CeleminArevalo/" target="_blank" rel="noopener noreferrer"
              className="social-btn github">
              <Github className="w-5 h-5" /> GitHub
            </a>
            <a href="mailto:s.sanchezcelemin@gmail.com" className="social-btn email">
              <Mail className="w-5 h-5" /> Email
            </a>
          </div>
          
          <div className="info-card mt-8">
            <p className="info-row"><Phone className="w-5 h-5 text-cyan-400" /> <span>+34 638 85 64 13</span></p>
            <p className="info-row"><MapPin className="w-5 h-5 text-cyan-400" /> <span>Ciudad Real / Corral de Almaguer</span></p>
            <p className="info-row"><Briefcase className="w-5 h-5 text-cyan-400" /> <span>Carnet B · Disponibilidad para viajar</span></p>
          </div>
          
          <a href="cv/Curriculum_Santiago_Sánchez-Celemín_Arévalo.pdf" download className="download-cv-btn mt-6">
            <Download className="w-5 h-5" /> Descargar CV
          </a>
        </div>
      </div>
    ),
    
    mouse: (
      <div className="section-content" data-testid="content-mouse">
        <h2 className="section-title" style={{ color: '#39ff14' }}>
          <User className="w-8 h-8" /> Sobre Mí
        </h2>
        <div className="prose-content">
          <p>Soy Santiago, estudiante del Grado en Ingeniería Informática en la UCLM, con especial interés en el desarrollo de software y tecnologías como <strong>Java, Spring Boot, Angular, SQL y Python</strong>.</p>
          <p>Me considero una persona con una gran <strong>capacidad de aprendizaje rápido</strong>, capaz de adaptarme a nuevos entornos y herramientas con facilidad.</p>
          <p>Disfruto trabajando en equipo, aportando soluciones y manteniendo siempre una <strong>actitud proactiva y orientada a resultados</strong>. Además, estoy acostumbrado a trabajar con <strong>Git y metodologías ágiles (Scrum)</strong> en equipo.</p>
          <p>Cuento también con formación complementaria en <strong>bases de datos, cloud e inteligencia artificial</strong>, lo que me permite abordar proyectos de forma eficaz.</p>
        </div>
      </div>
    ),
    
    keyboard: (
      <div className="section-content" data-testid="content-keyboard">
        <h2 className="section-title" style={{ color: '#ff00aa' }}>
          <Code className="w-8 h-8" /> Habilidades
        </h2>
        <div className="skills-grid">
          <div className="skill-card">
            <h3><Monitor className="w-5 h-5" /> Habilidades Técnicas</h3>
            <ul>
              {[
                { label: 'Lenguajes', value: 'Java, C++, Python' },
                { label: 'Backend', value: 'Java 17, Spring Boot, APIs REST, JSON' },
                { label: 'Bases de datos', value: 'MySQL, PostgreSQL, Oracle SQL, MongoDB' },
                { label: 'Front-end', value: 'HTML5, CSS3, Angular, consumo de APIs REST' },
                { label: 'Cloud & DevOps', value: 'Git/GitHub, Docker, CI/CD, AWS, Azure' },
                { label: 'IA y datos', value: 'Python para Machine Learning, Pandas' },
                { label: 'Ofimática', value: 'Microsoft Office (nivel avanzado)' }
              ].map((skill, i) => (
                <li key={i}><CheckCircle className="w-4 h-4" /> <strong>{skill.label}:</strong> {skill.value}</li>
              ))}
            </ul>
          </div>
          <div className="skill-card">
            <h3><User className="w-5 h-5" /> Habilidades No Técnicas</h3>
            <ul>
              {[
                'Comunicación clara y documentación técnica',
                'Trabajo en equipo y actitud proactiva',
                'Organización y gestión del tiempo',
                'Capacidad de aprendizaje rápido y adaptación',
                'Mentoría y apoyo a compañeros (Programa Mentor UCLM)'
              ].map((skill, i) => (
                <li key={i}><CheckCircle className="w-4 h-4" /> {skill}</li>
              ))}
            </ul>
          </div>
        </div>
      </div>
    ),
    
    coffee: (
      <div className="section-content" data-testid="content-coffee">
        <h2 className="section-title" style={{ color: '#ff6b35' }}>
          <GraduationCap className="w-8 h-8" /> Formación
        </h2>
        <div className="education-list">
          {[
            {
              title: 'Ingeniería Informática',
              institution: 'Universidad de Castilla-La Mancha (UCLM), Ciudad Real',
              period: '2022 — 2025 (en curso)',
              description: 'Estudiante del Grado en Ingeniería Informática. Formación orientada a desarrollo de software, bases de datos y sistemas.',
              icon: GraduationCap
            },
            {
              title: 'Bachillerato',
              institution: 'Humanidades y Ciencias Sociales',
              period: '2020 — 2022',
              description: '',
              icon: BookOpen
            }
          ].map((edu, i) => (
            <motion.div key={i} className="education-item" initial={{ opacity: 0, x: -20 }} animate={{ opacity: 1, x: 0 }} transition={{ delay: i * 0.1 }}>
              <edu.icon className="edu-icon" />
              <div>
                <h3>{edu.title}</h3>
                <p className="institution">{edu.institution}</p>
                <p className="period"><Calendar className="w-4 h-4" /> {edu.period}</p>
                {edu.description && <p className="description">{edu.description}</p>}
              </div>
            </motion.div>
          ))}
        </div>
      </div>
    ),
    
    headphones: (
      <div className="section-content" data-testid="content-headphones">
        <h2 className="section-title" style={{ color: '#ffd700' }}>
          <Languages className="w-8 h-8" /> Idiomas
        </h2>
        <div className="languages-grid">
          {[
            { flag: '🇪🇸', name: 'Español', level: 'Nativo' },
            { flag: '🇬🇧', name: 'Inglés', level: 'Profesional' }
          ].map((lang, i) => (
            <motion.div key={i} className="language-card" whileHover={{ scale: 1.05, y: -5 }}>
              <span className="flag">{lang.flag}</span>
              <h3>{lang.name}</h3>
              <p>{lang.level}</p>
            </motion.div>
          ))}
        </div>
      </div>
    ),
    
    gamepad: (
      <div className="section-content" data-testid="content-gamepad">
        <h2 className="section-title" style={{ color: '#ff69b4' }}>
          <Award className="w-8 h-8" /> Cursos y Formación Complementaria
        </h2>
        <p className="text-white/50 text-sm mb-4">Haz clic en un curso para verlo. Puedes previsualizar el certificado antes de descargar.</p>
        <div className="courses-list">
          {[
            { title: 'Desarrollo de App\'s Móviles', institution: 'Universidad Complutense de Madrid (2023)', skills: 'desarrollo móvil, publicación y deploy', logo: 'images/Complutense.jpg', cert: 'certs/Desarrollo_de_Apps_Móviles.pdf' },
            { title: 'Ciberseguridad', institution: 'INCIBE — Instituto Nacional de Ciberseguridad (2023)', skills: 'búsqueda de ciberamenazas, ciberdefensa', logo: 'images/INCIBE.jpg', cert: 'certs/CIBERSEGURIDAD.pdf' },
            { title: 'Digitaliza tu empresa', institution: 'Google (2023)', skills: 'digitalización, redes sociales, marketing digital, YouTube Analytics', logo: 'images/Google.jpg', cert: 'certs/DIGITALIZA_TU_NEGOCIO.pdf' },
            { title: 'Oracle Database', institution: 'Udemy (2025)', skills: 'modelado y diseño de BD, SQL, PL/SQL, administración', logo: 'images/Udemy.png', cert: 'certs/Certificado_Bases_de_Datos_Oracle.pdf' },
            { title: 'Agilidad con IA / Sostenibilidad', institution: 'CIBSE (2025)', skills: 'IA en ciclos ágiles, sostenibilidad en software', logo: 'images/CIBSE.png', cert: 'certs/Agilidad_con_esteroides_de_Inteligencia_Artificial.pdf' },
            { title: 'Software Engineering for ML Systems', institution: 'CIBSE (2025)', skills: 'Ingeniería de software aplicada a ML, pipelines, monitorización', logo: 'images/CIBSE.png', cert: 'certs/Software_Engineering_for_ML_Systems.pdf' },
            { title: 'Python: ML / Limpieza de datos', institution: 'Udemy (2025)', skills: 'modelado predictivo, limpieza con Pandas', logo: 'images/Udemy.png', cert: 'certs/Udemy,_transformación_y_limpieza_de_datos.pdf' },
            { title: 'Certificado Mentor', institution: 'UCLM', skills: 'Asesoramiento de labores administrativas, docentes y sociales', logo: 'images/UCLM.png', cert: 'certs/DIPLOMA_MENTOR.pdf' }
          ].map((course, i) => (
            <motion.div key={i} className="course-item-card" initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: i * 0.05 }}>
              <div className="course-logo-wrapper">
                <img src={course.logo} alt={course.institution} className="course-logo-img" onError={(e) => e.target.style.display='none'} />
              </div>
              <div className="course-info">
                <h3>{course.title}</h3>
                <p className="institution">{course.institution}</p>
                <p className="skills">Skills: {course.skills}</p>
                <div className="course-actions">
                  <a href={course.cert} target="_blank" rel="noopener noreferrer" className="cert-btn">
                    <FileText className="w-4 h-4" /> Abrir
                  </a>
                  <a href={course.cert} download className="cert-btn">
                    <Download className="w-4 h-4" /> Descargar
                  </a>
                </div>
              </div>
            </motion.div>
          ))}
        </div>
      </div>
    ),
    
    tower: (
      <div className="section-content" data-testid="content-tower">
        <h2 className="section-title" style={{ color: '#b266ff' }}>
          <Code className="w-8 h-8" /> Proyectos
        </h2>
        
        <div className="project-tabs">
          <button className={`tab-btn ${activeTab === 'uni' ? 'active' : ''}`} onClick={() => setActiveTab('uni')}>Universitarios</button>
          <button className={`tab-btn ${activeTab === 'gen' ? 'active' : ''}`} onClick={() => setActiveTab('gen')}>Generales</button>
        </div>
        
        <AnimatePresence mode="wait">
          {activeTab === 'uni' && (
            <motion.div key="uni" className="projects-list" initial={{ opacity: 0 }} animate={{ opacity: 1 }} exit={{ opacity: 0 }}>
              {[
                { 
                  title: 'GesstiFest', 
                  course: 'Interacción Persona-Ordenador', 
                  tech: 'WPF, GUI, usabilidad', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/GesstiFest'
                },
                { 
                  title: 'Lab_C1_SanchezPalomino', 
                  course: 'Arquitectura de Computadores', 
                  tech: 'OpenMP, DPC, Hiperespectral', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/Lab_C1_SanchezPalomino-main'
                },
                { 
                  title: '2024-ISO2-BC5', 
                  course: 'Ingeniería de Software 2', 
                  tech: 'PUD, GitHub, Maven, MySQL, POI', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/2024-ISO2-BC5-main'
                },
                { 
                  title: 'remote-types', 
                  course: 'Sistemas Distribuidos', 
                  tech: 'ZeroC Ice, JSON, Apache Kafka', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/remote-types-main-main'
                },
                { 
                  title: 'Sokoban_SSII', 
                  course: 'Sistemas Inteligentes', 
                  tech: 'Algoritmos de búsqueda, Python, Backtraking, Evaluación de rendimiento, Espacio de estados', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/Sokoban_SSII'
                },
                { 
                  title: 'Digital Builders 2025 — ESI & NTT DATA (3º Puesto)', 
                  course: 'Proyecto universitario Scrum', 
                  tech: 'Metodología ágil, MVP, stakeholders, gestión de proyectos, BackLog', 
                  highlight: true,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/Digital_Builders_2025',  // Cambia esto si tienes el enlace
                  demo: null  // Si tienes una demo en vivo, añádela aquí
                },
                { 
                  title: 'Proyecto Completo Gestión Proyectos', 
                  course: 'Gestión Integral de Proyectos Software', 
                  tech: 'Scrum, planificación, control de calidad, eMarisma, Sonarqube, Azure', 
                  highlight: false,
                  github: 'https://github.com/SantiagoSanchez-CeleminArevalo/Portfolio_SantiagoSanchez-CeleminArevalo/tree/projects/projects/Proyecto_Completo_Gestion_Proyectos'  // Cambia esto si tienes el enlace
                }
              ].map((project, i) => (
                <motion.div key={i} className={`project-card ${project.highlight ? 'highlight' : ''}`} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: i * 0.05 }}>
                  <h3>{project.highlight && <Award className="w-5 h-5 inline mr-2" />}{project.title}</h3>
                  <p className="course">{project.course}</p>
                  <p className="tech">{project.tech}</p>
                  <div className="project-links">
                    {project.github && (
                      <a href={project.github} target="_blank" rel="noopener noreferrer" className="github-link">
                        <Github className="w-4 h-4" /> GitHub <ExternalLink className="w-3 h-3" />
                      </a>
                    )}
                    {project.demo && (
                      <a href={project.demo} target="_blank" rel="noopener noreferrer" className="demo-link">
                        <ExternalLink className="w-4 h-4" /> Demo
                      </a>
                    )}
                    {!project.github && !project.demo && (
                      <span className="no-link">Repositorio privado</span>
                    )}
                  </div>
                </motion.div>
              ))}
            </motion.div>
          )}
          {activeTab === 'gen' && (
            <motion.div key="gen" className="empty-state" initial={{ opacity: 0 }} animate={{ opacity: 1 }}>
              No hay proyectos generales añadidos aún.
            </motion.div>
          )}
        </AnimatePresence>
      </div>
    ),
    
    phone: (
      <div className="section-content" data-testid="content-phone">
        <h2 className="section-title" style={{ color: '#00ffff' }}>
          <Mail className="w-8 h-8" /> Contacto
        </h2>
        <div className="contact-grid">
          {[
            { icon: Phone, title: 'Teléfono', value: '+34 638 85 64 13', href: 'tel:+34638856413' },
            { icon: Mail, title: 'Email', value: 's.sanchezcelemin@gmail.com', href: 'mailto:s.sanchezcelemin@gmail.com' },
            { icon: MapPin, title: 'Direcciones', value: 'Corral de Almaguer / Ciudad Real', href: null },
            { icon: Linkedin, title: 'LinkedIn', value: 'santiago-sánchez-celemín-arévalo', href: 'https://www.linkedin.com/in/santiago-s%C3%A1nchez-celem%C3%ADn-ar%C3%A9valo/' }
          ].map((contact, i) => (
            <motion.div key={i} className="contact-card" whileHover={{ scale: 1.02 }}>
              <contact.icon className="contact-icon" />
              <h3>{contact.title}</h3>
              {contact.href ? (
                <a href={contact.href} target={contact.href.startsWith('http') ? '_blank' : undefined} rel="noopener noreferrer">{contact.value}</a>
              ) : (
                <p>{contact.value}</p>
              )}
            </motion.div>
          ))}
        </div>
        
        <div className="social-buttons-row">
          <a href="https://www.linkedin.com/in/santiago-s%C3%A1nchez-celem%C3%ADn-ar%C3%A9valo/" target="_blank" rel="noopener noreferrer" className="social-btn-large linkedin">
            <Linkedin className="w-5 h-5" /> LinkedIn
          </a>
          <a href="https://santiagosanchez-celeminarevalo.github.io/Portfolio_SantiagoSanchez-CeleminArevalo/" target="_blank" rel="noopener noreferrer" className="social-btn-large github">
            <Github className="w-5 h-5" /> GitHub
          </a>
          <a href="mailto:s.sanchezcelemin@gmail.com" className="social-btn-large email">
            <Mail className="w-5 h-5" /> Email
          </a>
        </div>
      </div>
    )
  };
  
  return contents[sectionId] || <p className="text-white/50 text-center py-10">Selecciona un dispositivo del escritorio</p>;
};

// Realistic desk item component
const DeskItem = ({ item, onClick, isActive, style }) => {
  const IconComponent = item.icon;
  
  return (
    <motion.div
      data-testid={`desk-item-${item.id}`}
      className={`desk-item ${isActive ? 'active' : ''}`}
      style={{ ...style, '--item-color': item.color }}
      onClick={() => onClick(item.id)}
      whileHover={{ scale: 1.08, y: -5 }}
      whileTap={{ scale: 0.95 }}
    >
      <div className="item-body">
        <IconComponent className="item-icon" style={{ color: isActive ? item.color : '#888' }} />
        <span className="item-label">{item.title}</span>
      </div>
      {isActive && <div className="active-indicator" style={{ backgroundColor: item.color }}></div>}
    </motion.div>
  );
};

function App() {
  const [activeSection, setActiveSection] = useState('monitor');
  const [showWelcome, setShowWelcome] = useState(true);

  const handleItemClick = (id) => {
    setActiveSection(id);
  };

  return (
    <div className="desk-portfolio">
      <AnimatePresence>
        {showWelcome && (
          <motion.div 
            className="welcome-overlay"
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
          >
            <motion.div 
              className="welcome-modal"
              initial={{ scale: 0.9, y: 20 }}
              animate={{ scale: 1, y: 0 }}
            >
              <Monitor className="welcome-icon" />
              <h2>Bienvenido a mi Escritorio</h2>
              <p>Explora mi portafolio haciendo clic en los dispositivos del escritorio. Cada objeto contiene información sobre mí.</p>
              <motion.button
                data-testid="start-btn"
                className="start-btn"
                onClick={() => setShowWelcome(false)}
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
              >
                Encender Sistema
              </motion.button>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Main Layout */}
      <div className="desk-layout">
        {/* Left sidebar - Desk items */}
        <aside className="desk-sidebar">
          <div className="sidebar-header">
            <h3>Mi Escritorio</h3>
            <span className="online-indicator"></span>
          </div>
          <nav className="desk-nav">
            {Object.values(DESK_ITEMS).map((item) => (
              <DeskItem
                key={item.id}
                item={item}
                onClick={handleItemClick}
                isActive={activeSection === item.id}
              />
            ))}
          </nav>
        </aside>

        {/* Main content area - The "monitor" display */}
        <main className="desk-main">
          <div className="monitor-frame">
            <div className="monitor-bezel">
              {/* Monitor top bar */}
              <div className="monitor-topbar">
                <div className="window-controls">
                  <span className="control red"></span>
                  <span className="control yellow"></span>
                  <span className="control green"></span>
                </div>
                <span className="monitor-title">{DESK_ITEMS[activeSection]?.title || 'Escritorio'}</span>
                <div className="monitor-status">
                  <span className="status-dot"></span>
                  Online
                </div>
              </div>
              
              {/* Monitor screen content */}
              <div className="monitor-display">
                <AnimatePresence mode="wait">
                  <motion.div
                    key={activeSection}
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: -20 }}
                    transition={{ duration: 0.3 }}
                    className="display-content"
                  >
                    <SectionContent sectionId={activeSection} />
                  </motion.div>
                </AnimatePresence>
              </div>
            </div>
            
            {/* Monitor stand */}
            <div className="monitor-stand">
              <div className="stand-neck"></div>
              <div className="stand-base"></div>
            </div>
          </div>
        </main>
      </div>

      {/* Desk surface decoration */}
      <div className="desk-surface">
        <div className="desk-texture"></div>
      </div>
    </div>
  );
}

export default App;
