PRJ = LBL_FAD
CC = icpx

# Fuentes y objetos
SRCS  = Main_Detector_HW.cpp LBL_FAD_Transform_Operations.cpp Utils.cpp
OBJS = $(SRCS:.cpp=.o)

# Añadir las rutas necesarias para DPC++ y los archivos de cabecera
CPPFLAGS += -I/opt/intel/oneapi/compiler/latest/include -I/opt/intel/oneapi/vtune/latest/include -I/opt/intel/oneapi/advisor/latest/include

CPPFLAGS += -fsycl  # Habilita DPC++
LDFLAGS += -D VERBOSE -D VERBOSE_DBG -D BLOCK_DBG=-1
%.o : %.cpp
	$(CC) $(CPPFLAGS) $(LDFLAGS) -c $^

$(PRJ): $(OBJS)
	$(CC) $(LDFLAGS) $(CPPFLAGS) $^ -o $@

# Ejecutar el ejecutable
run: $(PRJ)
	./$(PRJ)

# Limpiar los archivos generados
clean:
	$(RM) -rf $(PRJ) *~ *.o admap.bin output

# Limpiar todo, incluyendo archivos binarios
cleanall: clean
	$(RM) -rf mission.bin
