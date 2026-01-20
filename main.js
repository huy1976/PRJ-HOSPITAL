async function searchInfo() {
        const type = document.getElementById('userType').value; 
        const inputId = document.getElementById('userId').value.trim();
        const resultArea = document.getElementById('resultArea');
        const errorMsg = document.getElementById('errorMessage');

        // Reset giao diện
        resultArea.style.display = 'none';
        errorMsg.style.display = 'none';

        if (!inputId) {
            alert("Vui lòng nhập ID!");
            return;
        }
        //gọi API
        let apiUrl = "";
        
        if (type === 'doctor') {
            apiUrl = `https://localhost:7088/api/Doctors?doctorId=${inputId}`; 
        } else {
            apiUrl = `https://localhost:7088/api/Patients?patientId=${inputId}`; 
        }

        try {
            const response = await fetch(apiUrl, {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            });

            if (!response.ok) throw new Error("Không tìm thấy");

            const data = await response.json();
            console.log("Dữ liệu nhận được:", data);  //check console
    
            resultArea.style.display = 'block';
                
           if (type === 'doctor') {    
                document.getElementById('labelDetail').innerText = "Bệnh viện - Địa chỉ:";
                document.getElementById('labelStatus').innerText = "Số điện thoại:";
                document.getElementById('resName').innerText = data.doctorFullName1;             
                document.getElementById('resDetail').innerText = (data.hospitalName || "") + " - " + (data.locationn || "");
                document.getElementById('resStatus').innerText = data.phoneNumber;
            } 
            else {
                document.getElementById('labelDetail').innerText = "Email liên hệ:";
                document.getElementById('labelStatus').innerText = "Trạng thái:";

                document.getElementById('resName').innerText = data.patientName;
                document.getElementById('resDetail').innerText = data.email;
                document.getElementById('resStatus').innerText = `SĐT: ${data.phoneNumber} | Đã đặt: ${data.appointmentCount} cuộc hẹn`;
            }
        } catch (error) {
            console.error(error);
            errorMsg.style.display = 'block';
            errorMsg.innerText = "Không tìm thấy dữ liệu hoặc lỗi kết nối Server!";
        }
    }