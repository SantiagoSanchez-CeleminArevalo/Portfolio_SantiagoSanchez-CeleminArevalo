#include <CL/sycl.hpp>


// This allow us to remove cl::sycl:: from all invokations
using namespace cl::sycl;

int main() {
    gpu_selector selector;

    queue q(selector);

    std::cout << "Device: " << q.get_device().get_info<info::device::name>()<< std::endl;
    std::cout << "Vendor: " << q.get_device().get_info<info::device::vendor>()<< std::endl;
    std::cout << "Max Clock Frequency: " << q.get_device().get_info<info::device::max_clock_frequency>()<< std::endl;
    std::cout << "Local Mem Size: " << q.get_device().get_info<info::device::local_mem_size>()<< std::endl;
    std::cout << "Driver Version: " << q.get_device().get_info<info::device::driver_version>()<< std::endl;
    std::cout << "Version: " << q.get_device().get_info<info::device::version>()<< std::endl;

    return 0;
}